using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    class Const : Var
    {
        private object Value;
        public Const(string name, string typeValue, object value) : base(name,typeValue)
        {
            TypeData = TypeData.CONSTS;
            Value = value;         
        }
    }
}
