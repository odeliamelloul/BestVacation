using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class SiteOwner1 : IClonable
    {
        private static int passwords;
        public static int Passwords { get => passwords; set => passwords = value; }
        private static int commission;
        public static int Commission  { get => commission; set => commission = value;}
        private static int accumulation;
        public static int Accumulation { get => accumulation; set => accumulation = value; }
        public override string ToString()
        {
                string siteOwner = " ";
                return siteOwner += string.Format("SiteOwner Passwords : {0}\n" + "SiteOwner Commission : {1}\n"+ "SiteOwner Accumulation : {2}\n", passwords, commission, accumulation);
            
        }
    }
}
