using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using LINQPad.Extensibility.DataContext;
using System.Data.Common;
using AzureCosmosDbDriver.Common;

namespace AzureCosmosDbDriver.Static
{

    public class Driver : StaticDataContextDriver
    {
        public override string Name => "CosmosDb Static Driver";

        public override string Author => DriverHelper.AuthorName;

        public override string GetConnectionDescription(IConnectionInfo cxInfo) => DriverHelper.GetConnectionDescription(cxInfo);

        public override DbProviderFactory GetProviderFactory(IConnectionInfo cxInfo) => DriverHelper.GetProviderFactory(cxInfo);

        public override IDbConnection GetIDbConnection(IConnectionInfo cxInfo) => DriverHelper.GetIDbConnection(cxInfo);

        public override bool ShowConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection) => new Static.ConnectionDialog(cxInfo).ShowDialog() == true;

        public override List<ExplorerItem> GetSchema(IConnectionInfo cxInfo, Type customType) => SchemaBuilder.GetSchema(new Properties(cxInfo));

        public override void TearDownContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager, object[] constructorArguments) => ((IDisposable)context).Dispose();

        public override bool AreRepositoriesEquivalent(IConnectionInfo c1, IConnectionInfo c2) => DriverHelper.AreRepositoriesEquivalent(c1, c2);

        public override ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo) => DriverHelper.GetContextConstructorParameters(cxInfo);

        public override object[] GetContextConstructorArguments(IConnectionInfo cxInfo) => DriverHelper.GetContextConstructorArguments(cxInfo);

        public override void InitializeContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
        {
            var typedCtx = (CosmosDbContext.CosmosDbContext)context;
            typedCtx.Log += query => executionManager.SqlTranslationWriter.WriteLine(query);
        }
    }
}
