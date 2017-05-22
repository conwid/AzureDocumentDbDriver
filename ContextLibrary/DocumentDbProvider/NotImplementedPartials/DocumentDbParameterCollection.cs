using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    partial class DocumentDbParameterCollection
    {
        public override object SyncRoot
        {
            get
            {
                throw new OperationNotImplementedException();
            }
        }

        public override int Add(object value)
        {
            throw new OperationNotImplementedException();
        }

        public override void AddRange(Array values)
        {
            throw new OperationNotImplementedException();
        }

        public override void Clear()
        {
            throw new OperationNotImplementedException();
        }

        public override bool Contains(string value)
        {
            throw new OperationNotImplementedException();
        }

        public override bool Contains(object value)
        {
            throw new OperationNotImplementedException();
        }

        public override void CopyTo(Array array, int index)
        {
            throw new OperationNotImplementedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new OperationNotImplementedException();
        }

        public override int IndexOf(string parameterName)
        {
            throw new OperationNotImplementedException();
        }

        public override int IndexOf(object value)
        {
            throw new OperationNotImplementedException();
        }

        public override void Insert(int index, object value)
        {
            throw new OperationNotImplementedException();
        }

        public override void Remove(object value)
        {
            throw new OperationNotImplementedException();
        }

        public override void RemoveAt(string parameterName)
        {
            throw new OperationNotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new OperationNotImplementedException();
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            throw new OperationNotImplementedException();
        }

        protected override DbParameter GetParameter(int index)
        {
            throw new OperationNotImplementedException();
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            throw new OperationNotImplementedException();
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            throw new OperationNotImplementedException();
        }
    }
}
