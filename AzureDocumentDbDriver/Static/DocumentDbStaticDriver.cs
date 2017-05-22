using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using LINQPad.Extensibility.DataContext;
using System.Data.Common;
using ContextLibrary.DocumentDbProvider;
using AzureDocumentDbDriver.Common;

namespace AzureDocumentDbDriver.Static
{

    public class Driver : StaticDataContextDriver
    {
        public override string Name { get { return "DocumentDb Static Driver"; } }

        public override string Author { get { return DriverHelper.AuthorName; } }

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
            return new Static.ConnectionDialog(cxInfo).ShowDialog() == true;
        }

        public override List<ExplorerItem> GetSchema(IConnectionInfo cxInfo, Type customType)
        {
            return SchemaBuilder.GetSchema(new Properties(cxInfo));
        }

        public override void TearDownContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager, object[] constructorArguments)
        {
            ((IDisposable)context).Dispose();
        }

        public override bool AreRepositoriesEquivalent(IConnectionInfo c1, IConnectionInfo c2)
        {
            return DriverHelper.AreRepositoriesEquivalent(c1, c2);
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
