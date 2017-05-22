using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQPad.Extensibility.DataContext;
using System.Dynamic;
using System.Data.Common;
using ContextLibrary.DocumentDbProvider;
using System.Data;

namespace AzureDocumentDbDriver.Common
{
    public static class DriverHelper
    {
        public static string AuthorName { get { return "Akos Nagy"; } }

        public static IEnumerable<string> GetAssembliesToAdd()
        {
            return new List<string> { "Microsoft.Azure.Documents.Client.dll", "Newtonsoft.Json.dll", "ContextLibrary.dll" };
        }

        public static IEnumerable<string> GetNamespaceToAdd()
        {
            return new List<string> { "Microsoft.Azure.Documents.Client", "ContextLibrary", "System.Dynamic", "System.Collections.Generic" };
        }

        public static string GetConnectionDescription(IConnectionInfo cxInfo)
        {
            var props = new Properties(cxInfo);
            List<ExpandoObject> o = new List<ExpandoObject>();
            return $"{props.Uri}/{props.Database}";
        }

        public static ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo)
        {
            var stringTypeName = typeof(System.String).FullName;
            return new[] {
                new ParameterDescriptor("uri", stringTypeName),
                new ParameterDescriptor("authKey", stringTypeName),
                new ParameterDescriptor("database", stringTypeName)
            };
        }

        internal static IDbConnection GetIDbConnection(IConnectionInfo cxInfo)
        {
            Properties props = new Properties(cxInfo);
            return new DocumentDbConnection(props.Uri, props.AccountKey, props.Database);
        }

        public static DbProviderFactory GetProviderFactory(IConnectionInfo cxInfo)
        {
            if (cxInfo.DatabaseInfo.Provider != DocumentDbProviderFactory.ProviderName)
            {
                throw new NotSupportedException($"Only {DocumentDbProviderFactory.ProviderName} is supported; requested {cxInfo.DatabaseInfo.Provider}");
            }
            Properties props = new Properties(cxInfo);
            return new DocumentDbProviderFactory(props.Uri, props.AccountKey, props.Database);
        }

        public static object[] GetContextConstructorArguments(IConnectionInfo cxInfo)
        {
            var props = new Properties(cxInfo);
            return new object[] { props.Uri, props.AccountKey, props.Database };
        }

        internal static bool AreRepositoriesEquivalent(IConnectionInfo c1, IConnectionInfo c2)
        {
            var p1 = new Properties(c1);
            var p2 = new Properties(c2);
            return p1.AccountKey == p2.AccountKey;
        }        
    }
}
