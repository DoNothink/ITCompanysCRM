global using WpfClasses;
global using ITCompanysCRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCompanysCRM.ClassFolder
{
    internal static class GlobalClass
    {
        private static User globalUser;

        public static User GlobalUser
        {
            get { return globalUser; }
            set { globalUser = value; }
        }
    }
}
