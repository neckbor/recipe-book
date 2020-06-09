using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class AuthOptions
    {
        public const string ISSUER = "YummYummY-Backend"; // издатель токена
        public const string AUDIENCE = "YummYummY-User"; // потребитель токена
        const string KEY = "no1body.can-hack!YummYummY.Backend-matherf88ckers";   // ключ для шифрации
        public const int LIFETIME = 300000; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
