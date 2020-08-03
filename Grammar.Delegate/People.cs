using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammar.Delegate
{
    public abstract class People
    {
        public event Action<string> Before;
        public event Action<string> After;
    }
}
