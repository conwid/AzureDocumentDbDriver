using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class DocumentDbConnection : DbConnection
    {
        private string databaseName = "";
        private string serviceUri = "";
        private string authKey = "";
        public readonly DocumentClient client;

        public DocumentDbConnection() : base()
        {

        }

        public DocumentDbConnection(string connectionString) : base()
        {
            var parts = connectionString.Split(';');
            serviceUri = parts.Single(p => p.StartsWith("AccountEndpoint")).Split('=')[1];
            authKey = parts.Single(p => p.StartsWith("AccountKey")).Split('=')[1];
            databaseName = parts.Single(p => p.StartsWith("Database")).Split('=')[1];
        }

        public override string ConnectionString
        {
            get
            {
                return $"AccountEndpoint={serviceUri};AccountKey={authKey};Database={databaseName};";
            }

            set
            {
                var parts = value.Split(';');
                serviceUri = parts.Single(p => p.StartsWith("AccountEndpoint")).Split('=')[1];
                authKey = parts.Single(p => p.StartsWith("AccountKey")).Split('=')[1];
                databaseName = parts.Single(p => p.StartsWith("Database")).Split('=')[1];
            }
        }

        public override string Database
        {
            get
            {
                return databaseName;
            }
        }

        public override string DataSource
        {
            get
            {
                return serviceUri;
            }
        }

        public override string ServerVersion
        {
            get
            {
                return "1.0";
            }
        }

        private bool isOpen = false;
        public override ConnectionState State
        {
            get
            {
                if (isOpen)
                {
                    return ConnectionState.Open;
                }
                else
                {
                    return ConnectionState.Closed;
                }
            }
        }

        public DocumentDbConnection(string serviceUri, string authKey, string databaseName) : base()
        {
            this.databaseName = databaseName;
            this.authKey = authKey;
            this.serviceUri = serviceUri;
            this.client = new DocumentClient(new Uri(serviceUri), authKey);
        }

        public override void Close()
        {
            this.isOpen = false;
            this.client.Dispose();
        }

        public override void ChangeDatabase(string databaseName)
        {
            this.databaseName = databaseName;
        }

        protected override DbCommand CreateDbCommand()
        {
            return new DocumentDbCommand(this);
        }

        public override void Open()
        {
            this.isOpen = true;
        }
    }
}
