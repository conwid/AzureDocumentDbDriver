using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class DocumentDbCommand : DbCommand
    {
        private DocumentDbConnection conn;
        private string commandText;
        public DocumentDbCommand()
        {

        }
        public DocumentDbCommand(DocumentDbConnection conn)
        {
            this.conn = conn;
        }
        public DocumentDbCommand(DocumentDbConnection conn, string commandText)
        {
            this.commandText = commandText;
            this.conn = conn;
        }
        public override string CommandText
        {
            get
            {
                return this.commandText;
            }

            set
            {
                this.commandText = value;
            }
        }
        protected override DbConnection DbConnection
        {
            get
            {
                return this.conn;
            }

            set
            {
                this.conn = (DocumentDbConnection)value;
            }
        }
        private DocumentDbParameterCollection dbParameterCollection = new DocumentDbParameterCollection();
        protected override DbParameterCollection DbParameterCollection
        {
            get
            {
                return dbParameterCollection;
            }
        }

        public override void Cancel()
        {

        }

        public override object ExecuteScalar()
        {
            return ExecuteInternal()[0].ElementAt(0).Value;
        }

        public override void Prepare()
        {

        }

        protected override DbParameter CreateDbParameter()
        {
            return new DocumentDbParameter();
        }

        private int timeout;
        public override int CommandTimeout
        {
            get
            {
                return this.timeout;
            }

            set
            {
                if (this.timeout != 0)
                {
                    throw new NotSupportedException("Only a value of 0 is supported currently for timeout");
                }
                this.timeout = value;
            }
        }


        protected List<ExpandoObject> ExecuteInternal()
        {
            var cmdTextParts = this.commandText.Split(':');
            var collection = cmdTextParts[0];
            var cmdText = string.Join(":", cmdTextParts.Where((p, i) => i != 0));
            return this.conn.client
               .CreateDocumentQuery<ExpandoObject>(UriFactory.CreateDocumentCollectionUri(this.conn.Database, collection), cmdText)
               .ToList();

        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            var x = ExecuteInternal();
            return new DocumentDbDataReader(x);
        }

        private CommandType commandType;
        public override CommandType CommandType
        {
            get
            {
                return commandType;
            }
            set
            {
                if (value != CommandType.Text)
                {
                    throw new NotSupportedException("Only text command type is supported");
                }
                commandType = CommandType.Text;
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                return false;
            }

            set
            {
                throw new OperationNotImplementedException();
            }
        }

    }
}
