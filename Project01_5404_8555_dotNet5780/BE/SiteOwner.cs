using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class SiteOwner : IClonable
    {
        private static int commission = 10;
        public static int Commission  { get => commission; set => commission = value;}
        public override string ToString()
        {
                string siteOwner = " ";
                return siteOwner += string.Format("SiteOwner Commission : {0}\n", commission);
            
        }
    }
}
