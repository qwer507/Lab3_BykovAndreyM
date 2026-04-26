using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApp1.Classes
{
    public static class PasswordHasher
    {
        public static string MaskPassword(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return "***";

            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashString = Convert.ToBase64String(hash);
            return hashString;
        }
    }
}
