using System;
using System.Collections.Generic;
using System.Text;

namespace Grammar.Generic
{
    public abstract class People
    {
        private string _name;
        public People(string Name) => _name = Name;
        public void CallMyName()
        {
            Console.WriteLine($@"{nameof(this._name)}是{this._name}");
        }
    }
}
