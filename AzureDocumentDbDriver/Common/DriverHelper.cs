using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQPad.Extensibility.DataContext;
using System.Dynamic;
using System.Data.Common;
using System.Data;
using CosmosDbAdoNetProvider;

namespace AzureCosmosDbDriver.Common
{
    public static class DriverHelper
    {
        public static string AuthorName => "Akos Nagy";

        public static IEnumerable<string> GetAssembliesToAdd() =>
            new[]
            {
                "Microsoft.Azure.Documents.Client.dll",
                "Newtonsoft.Json.dll",
                "CosmosDbAdoNetProvider.dll",
                "CosmosDbContext.dll"
            };


        public static IEnumerable<string> GetNamespaceToAdd() =>
            new[]
            {
                 "Microsoft.Azure.Documents.Client",
                 "System.Dynamic",
                 "System.Collections.Generic",
                 "CosmosDbContext",
                 "CosmosDbContext.Collection"
            };
        

        public static string GetConnectionDescription(IConnectionInfo cxInfo)
        {
            var props = new Properties(cxInfo);            
            return $"{props.Uri}/{props.Database}";
        }

        public static ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo)
        {
            var stringTypeName = typeof(string).FullName;
            return new[] {
                new ParameterDescriptor("uri", stringTypeName),
                new ParameterDescriptor("authKey", stringTypeName),
                new ParameterDescriptor("database", stringTypeName)
            };
        }

        internal static IDbConnection GetIDbConnection(IConnectionInfo cxInfo)
        {
            var props = new Properties(cxInfo);
            return new CosmosDbSqlConnection(props.Uri, props.AccountKey, props.Database);
        }

        public static DbProviderFactory GetProviderFactory(IConnectionInfo cxInfo)
        {
            if (cxInfo.DatabaseInfo.Provider != CosmosDbSqlProviderFactory.ProviderName)
            {
                throw new NotSupportedException($"Only {CosmosDbSqlProviderFactory.ProviderName} is supported; requested {cxInfo.DatabaseInfo.Provider}");
            }
            var props = new Properties(cxInfo);
            return new CosmosDbSqlProviderFactory(props.Uri, props.AccountKey, props.Database);
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
