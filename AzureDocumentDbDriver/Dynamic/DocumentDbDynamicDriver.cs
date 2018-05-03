using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LINQPad.Extensibility.DataContext;
using System.Data;
using ContextLibrary;
using System.Data.Common;
using LINQPad;
using ContextLibrary.DocumentDbProvider;
using AzureDocumentDbDriver.Common;

namespace AzureDocumentDbDriver.Dynamic
{
    public class Driver : DynamicDataContextDriver
    {
        public override string Name { get { return "CosmosDb Dynamic Driver"; } }

        public override string Author { get { return DriverHelper.AuthorName; } }


        public override IEnumerable<string> GetAssembliesToAdd(IConnectionInfo cxInfo)
        {
            return base.GetAssembliesToAdd(cxInfo).Concat(DriverHelper.GetAssembliesToAdd());
        }

        public override IEnumerable<string> GetNamespacesToAdd(IConnectionInfo cxInfo)
        {
            return base.GetNamespacesToAdd(cxInfo).Concat(DriverHelper.GetNamespaceToAdd());
        }

        public override string GetConnectionDescription(IConnectionInfo cxInfo)
        {
            return DriverHelper.GetConnectionDescription(cxInfo);
        }


        public override DbProviderFactory GetProviderFactory(IConnectionInfo cxInfo)
        {
            return DriverHelper.GetProviderFactory(cxInfo);
        }

        public override IDbConnection GetIDbConnection(IConnectionInfo cxInfo)
        {
            return DriverHelper.GetIDbConnection(cxInfo);
        }

        public override bool ShowConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection)
        {
            return new Dynamic.ConnectionDialog(cxInfo).ShowDialog() == true;
        }


        public override bool AreRepositoriesEquivalent(IConnectionInfo c1, IConnectionInfo c2)
        {
            return DriverHelper.AreRepositoriesEquivalent(c1, c2);
        }
        public override List<ExplorerItem> GetSchemaAndBuildAssembly(IConnectionInfo cxInfo, AssemblyName assemblyToBuild, ref string nameSpace, ref string typeName)
        {
            return SchemaBuilder.GetSchemaAndBuildAssembly(
                new Properties(cxInfo),
                assemblyToBuild,
                ref nameSpace,
                ref typeName,
                this.GetDriverFolder()
             );
        }

        public override void TearDownContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager, object[] constructorArguments)
        {
            ((IDisposable)context).Dispose();
        }

        public override ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo)
        {
            return DriverHelper.GetContextConstructorParameters(cxInfo);
        }

        public override object[] GetContextConstructorArguments(IConnectionInfo cxInfo)
        {
            return DriverHelper.GetContextConstructorArguments(cxInfo);
        }
    }
}
