using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    class Classes : Identificator
    {
        public Classes(string name) : base(name)
        {
            Name = name;
            TypeData = TypeData.CLASSES;
            TypeValue = TypeValue.class_type;
        }
    }
}
