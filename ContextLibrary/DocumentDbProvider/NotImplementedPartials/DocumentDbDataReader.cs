using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    partial class DocumentDbDataReader
    {
        public override int Depth
        {
            get
            {
                throw new OperationNotImplementedException();
            }
        }

        public override int RecordsAffected
        {
            get
            {
                throw new OperationNotImplementedException();
            }
        }

        public override bool HasRows
        {
            get
            {
                throw new OperationNotImplementedException();
            }
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new OperationNotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new OperationNotImplementedException();
        }



        public override int GetOrdinal(string name)
        {
            throw new OperationNotImplementedException();
        }

        public override bool GetBoolean(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override byte GetByte(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new OperationNotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new OperationNotImplementedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override decimal GetDecimal(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override float GetFloat(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override short GetInt16(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override int GetInt32(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override long GetInt64(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override string GetString(int ordinal)
        {
            throw new OperationNotImplementedException();
        }

        public override int GetValues(object[] values)
        {
            throw new OperationNotImplementedException();
        }

        public override bool IsDBNull(int ordinal)
        {
            throw new OperationNotImplementedException();
        }
    }
}

