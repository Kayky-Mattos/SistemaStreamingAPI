using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace SistemaStreaming.Utils
{
    
    public class HashPassword
    {
        public static string GenerateHash(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedpassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedpassword);
        }
    }
}