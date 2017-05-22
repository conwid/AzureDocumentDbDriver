using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    public partial class DocumentDbDataReader : DbDataReader
    {
        private List<ExpandoObject> query;
        private int curentIndex = -1;
        public DocumentDbDataReader(List<ExpandoObject> query)
        {
            this.query = query;
        }

        public override object this[string name]
        {
            get
            {
                return query[curentIndex].Single(s => s.Key == name).Value;
            }
        }

        public override object this[int i]
        {
            get
            {
                return query[curentIndex].Where((s, index) => index == i).Single().Value;
            }
        }      

        public override int FieldCount
        {
            get
            {
                return query[curentIndex].Count();
            }
        }

        private bool closed = false;
        public override bool IsClosed
        {
            get
            {
                return this.closed;
            }
        }

        public override void Close()
        {
            this.closed = true;
        }

        public override bool Read()
        {
            curentIndex++;
            return curentIndex < query.Count;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override string GetName(int ordinal)
        {
            return query[0].Where((kp, index) => index == ordinal).Single().Key;
        }

        public override object GetValue(int ordinal)
        {
            return this[ordinal];
        }
    }
}
