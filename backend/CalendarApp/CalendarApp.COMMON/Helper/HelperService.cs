using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.COMMON.Helper
{
    public class HelperService
    {
        public static string GetPasswordEncode(string password)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string hashed = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hashed;
        }
    }
}
