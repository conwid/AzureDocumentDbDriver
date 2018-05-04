using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LINQPad.Extensibility.DataContext;
using System.Data;
using System.Data.Common;
using LINQPad;
using AzureCosmosDbDriver.Common;

namespace AzureCosmosDbDriver.Dynamic
{
    public class Driver : DynamicDataContextDriver
    {
        public override string Name => "CosmosDb Dynamic Driver"; 

        public override string Author => DriverHelper.AuthorName;

        public override IEnumerable<string> GetAssembliesToAdd(IConnectionInfo cxInfo) => base.GetAssembliesToAdd(cxInfo).Concat(DriverHelper.GetAssembliesToAdd());        

        public override IEnumerable<string> GetNamespacesToAdd(IConnectionInfo cxInfo) => base.GetNamespacesToAdd(cxInfo).Concat(DriverHelper.GetNamespaceToAdd());        

        public override string GetConnectionDescription(IConnectionInfo cxInfo) =>  DriverHelper.GetConnectionDescription(cxInfo);        

        public override DbProviderFactory GetProviderFactory(IConnectionInfo cxInfo) => DriverHelper.GetProviderFactory(cxInfo);        

        public override IDbConnection GetIDbConnection(IConnectionInfo cxInfo) => DriverHelper.GetIDbConnection(cxInfo);        

        public override bool ShowConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection) => new Dynamic.ConnectionDialog(cxInfo).ShowDialog() == true;        

        public override bool AreRepositoriesEquivalent(IConnectionInfo c1, IConnectionInfo c2) => DriverHelper.AreRepositoriesEquivalent(c1, c2);        
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

        public override void TearDownContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager, object[] constructorArguments) => ((IDisposable)context).Dispose();        

        public override ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo) => DriverHelper.GetContextConstructorParameters(cxInfo);
        
        public override object[] GetContextConstructorArguments(IConnectionInfo cxInfo) => DriverHelper.GetContextConstructorArguments(cxInfo);        
    }
}
