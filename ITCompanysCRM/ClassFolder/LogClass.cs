using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCompanysCRM.ClassFolder
{
    static class LogClass
    {
        public static void LogToDataBase(string message)
        {
            using (ItcompanysCrmdbContext db = new())
            {
                LogBook logBook = new LogBook()
                {
                    IdUser = GlobalClass.GlobalUser.IdUser,
                    IdRole = GlobalClass.GlobalUser.IdRole,
                    Description = message,
                    DateLog = DateTime.Now,
                };

                db.LogBooks.Add(logBook);
                db.SaveChangesAsync();
            }
        }
    }
}
