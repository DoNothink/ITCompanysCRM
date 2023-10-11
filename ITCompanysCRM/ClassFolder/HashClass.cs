using System;
using System.Security.Cryptography;
using System.Text;

namespace ITCompanysCRM.ClassFolder
{
    class HashClass
    {
        /// <summary>
        /// Метод для хэширования пароля в MD5 алгоритм
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>захэшированный пароль</returns>
        public static string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
            {
                sb.Append(a.ToString("X2"));
            }

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            return Convert.ToString(sb);
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        }
    }
}
