using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;

namespace DAL
{
     public class FactoryDal
    {
       public static IDAL instance = null;
        public static IDAL GetDal()
        {
            if (instance == null)
                instance = new Dal_imp();
            return  instance;
        }
    }
}
