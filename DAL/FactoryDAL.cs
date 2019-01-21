using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDAL
    {
        public static Idal Get_DAL()
        {
            return Dal_imp.Instance;
        }
    }
}
