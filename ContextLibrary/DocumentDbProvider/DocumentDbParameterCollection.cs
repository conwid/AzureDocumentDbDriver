using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    public partial class DocumentDbParameterCollection : DbParameterCollection
    {
        public override int Count
        {
            get
            {
                return 0;
            }
        }
    }
}
