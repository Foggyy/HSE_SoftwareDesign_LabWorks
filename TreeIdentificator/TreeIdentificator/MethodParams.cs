using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    class MethodParams
    {
        private TypeValue value;
        private ParametrType param;

        public MethodParams(string typeValue, ParametrType param)
        {
            this.param = param;

            switch (typeValue)
            {
                case "int": value = TypeValue.int_type; break;
                case "float": value = TypeValue.float_type; break;
                case "string": value = TypeValue.string_type; break;
                case "char": value = TypeValue.char_type; break;
                case "bool": value = TypeValue.bool_type; break;

            }
        }
    }
}
