using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    class Method : Var
    {
        private List<MethodParams> MyList;
        public Method(string name, string typeValue, List<MethodParams> List) : base(name,typeValue)
        {
            TypeData = TypeData.METHODS;
            MyList = List;
        }
    }
}
