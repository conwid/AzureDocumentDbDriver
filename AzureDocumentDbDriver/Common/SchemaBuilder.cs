using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using LINQPad.Extensibility.DataContext;
using Microsoft.CSharp;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace AzureCosmosDbDriver.Common
{
    public static class SchemaBuilder
    {
        public static List<ExplorerItem> GetSchema(Properties props)
        {
            using (DocumentClient client = new DocumentClient(new Uri(props.Uri), props.AccountKey))
            {
                var database = client.CreateDatabaseQuery().AsEnumerable().Single(db => db.Id == props.Database);
                ExplorerItem dbItem = new ExplorerItem(database.Id, ExplorerItemKind.Schema, ExplorerIcon.Schema)
                {
                    Children = new List<ExplorerItem>()
                };

                List<DocumentCollection> collections = client.CreateDocumentCollectionQuery(database.SelfLink).ToList();
                foreach (var collection in collections)
                {
                    var collectionItem = new ExplorerItem(collection.Id, ExplorerItemKind.QueryableObject, ExplorerIcon.Table) { IsEnumerable = true, Tag = collection.Id, Children = new List<ExplorerItem>() };
                    dbItem.Children.Add(collectionItem);
                    var sps = client.CreateStoredProcedureQuery(collection.SelfLink);
                    var storedProceduresCategory = new ExplorerItem("Stored procedures", ExplorerItemKind.Category, ExplorerIcon.StoredProc) { Children = new List<ExplorerItem>() };
                    foreach (var sp in sps)
                    {
                        storedProceduresCategory.Children.Add(new ExplorerItem(sp.Id, ExplorerItemKind.QueryableObject, ExplorerIcon.StoredProc) { IsEnumerable = true, Tag = sp.Id, DragText = sp.Id, ToolTipText = sp.Body });
                    }

                    var udfs = client.CreateUserDefinedFunctionQuery(collection.SelfLink);
                    var udfsCategory = new ExplorerItem("User-defined functions", ExplorerItemKind.Category, ExplorerIcon.ScalarFunction) { Children = new List<ExplorerItem>() };
                    foreach (var udf in udfs)
                    {
                        udfsCategory.Children.Add(new ExplorerItem(udf.Id, ExplorerItemKind.QueryableObject, ExplorerIcon.ScalarFunction) { IsEnumerable = true, Tag = udf.Id, DragText = "SELECT udf." + udf.Id, ToolTipText = udf.Body });
                    }
                    collectionItem.Children.Add(storedProceduresCategory);
                    collectionItem.Children.Add(udfsCategory);
                }


                return new List<ExplorerItem> { dbItem };
            }
        }
        public static List<ExplorerItem> GetSchemaAndBuildAssembly(Properties props, AssemblyName name,
              ref string nameSpace, ref string typeName, string driverFolder)
        {
            var items = GetSchema(props);
            string code = GenerateCode(items[0].Children, nameSpace, typeName, props.Database);
            BuildAssembly(code, name, driverFolder);
            return items;
        }

        private static string GenerateCode(IEnumerable<ExplorerItem> collections, string @namepace, string typeName, string database)
        {
            string code = $"namespace {@namepace} {{";
            code = code + "using Microsoft.Azure.Documents.Client;" +
                          "using System;" +
                          "using System.Linq; " +
                          "using System.Dynamic;" +
                          "using System.Collections.Generic;" +                          
                          "using CosmosDbContext.Collection;";

            code = code + $"public abstract class {typeName} : CosmosDbContext.CosmosDbContext" +
                           $"{{public {typeName}(string uri, string authKey, string database) : base (uri,authKey,database) {{}}";
            foreach (var collection in collections)
            {
                code = code + $" [CosmosDbCollection] public ICosmosDbCollection<dynamic> {collection.Tag} {{ get; set; }}";                                

                foreach (var sp in collection.Children.Single(s => s.Text == "Stored procedures").Children)
                {
                    code = code + $" public IEnumerable<dynamic> {sp.Tag}(params dynamic[] parameters)" +
                                "{" +
                                   $" return base.ExecuteDynamicStoredProcedure(\"{sp.Tag}\",\"{collection.Tag}\", parameters).ToList();" +
                                "}";
                }
            }
            code = code + "}}";
            return code;
        }


        private static void BuildAssembly(string code, AssemblyName name, string driverFolder)
        {
#if DEBUG
            bool includeDebug = true;
#else
            bool includeDebug = false;
#endif
            CompilerResults results;
            using (var codeProvider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } }))
            {
                var dependencies = new string[] {
                    "System.dll",
                    "System.Core.dll",
                    "System.Xml.dll",
                    Path.Combine(driverFolder,"Microsoft.Azure.Documents.Client.dll"),
                    Path.Combine(driverFolder,"Newtonsoft.Json.dll"),
                    Path.Combine(driverFolder,"CosmosDbContext.dll"),
                    Path.Combine(driverFolder,"CosmosDbAdoNetProvider.dll"),
                };
                var options = new CompilerParameters(dependencies, name.CodeBase, includeDebug);
                results = codeProvider.CompileAssemblyFromSource(options, code);
            }
            if (results.Errors.Count > 0)
                throw new Exception
                    ("Cannot compile typed context: " + results.Errors[0].ErrorText + " (line " + results.Errors[0].Line + ")");
        }
    }
}
