using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TreeIdentificator
{
    class Var :Identificator
    {
        public Var(string name, string typeValue) : base(name)
        {
            Name = name;
            TypeData = TypeData.VARS;
            switch (typeValue)
            {
                case "int" : TypeValue = TypeValue.int_type; break;
                case "float": TypeValue = TypeValue.float_type; break;
                case "string": TypeValue = TypeValue.string_type; break;
                case "char": TypeValue = TypeValue.char_type; break;
                case "bool": TypeValue = TypeValue.bool_type; break;

            }
        }   
    }
}
