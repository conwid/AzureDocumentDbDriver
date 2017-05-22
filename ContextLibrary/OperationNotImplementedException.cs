using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary
{
    public class OperationNotImplementedException : NotImplementedException
    {
        public OperationNotImplementedException([CallerMemberName] string memberName = "") : base($"Method or operation {memberName} is not implemented") { }
    }
}
