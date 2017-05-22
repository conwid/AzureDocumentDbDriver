using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    partial class DocumentDbCommand
    {      
        protected override DbTransaction DbTransaction
        {
            get
            {
                throw new OperationNotImplementedException();
            }

            set
            {
                throw new OperationNotImplementedException();
            }
        }

        public override int ExecuteNonQuery()
        {
            throw new OperationNotImplementedException();
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get
            {
                throw new OperationNotImplementedException();
            }

            set
            {
                throw new OperationNotImplementedException();
            }
        }        
    }
}
