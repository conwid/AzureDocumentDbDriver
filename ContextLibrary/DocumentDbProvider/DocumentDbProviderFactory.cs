using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    public class DocumentDbProviderFactory : DbProviderFactory
    {
        private string serviceUri;
        private string authKey;
        private string databaseName;

        public static readonly string ProviderName = "DocumentDbProvider";

        public DocumentDbProviderFactory(string serviceUri, string authKey, string databaseName)
        {
            this.serviceUri = serviceUri;
            this.authKey = authKey;
            this.databaseName = databaseName;
        }
        public override DbCommand CreateCommand()
        {
            return new DocumentDbCommand();
        }

        public override DbConnection CreateConnection()
        {
            return new DocumentDbConnection(serviceUri, authKey, databaseName);
        }

        public override DbParameter CreateParameter()
        {
            return new DocumentDbParameter();
        }


        public override DbDataAdapter CreateDataAdapter()
        {
            return new DocumentDbDataAdapter();
        }
    }
}
