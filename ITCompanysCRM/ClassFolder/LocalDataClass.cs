using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCompanysCRM.ClassFolder
{
    class LocalDataClass
    {
        public LocalDataClass()
        {
            string path = CreateApplicationFolder();

        }

        /// <summary>
        /// Создание папки приложения в %appdata%
        /// </summary>
        private string CreateApplicationFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, "ITCompanysCRM");
            Directory.CreateDirectory(specificFolder);
            return specificFolder;
        }
    }
}
