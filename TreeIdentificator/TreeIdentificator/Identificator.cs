using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    public enum TypeData { VARS, CONSTS, CLASSES, METHODS };
    public enum TypeValue { int_type, float_type, class_type, string_type, char_type, bool_type };

    public enum ParametrType {param_val,param_ref,param_out}

    public abstract class Identificator
    {
        public string Name;
        public int Hash
        {
            get
            {
                return Name.GetHashCode();
            }
        }

        public TypeData TypeData;
        public TypeValue TypeValue;

        public Identificator(string name)
        {
            Name = name;
        }
    }
}
