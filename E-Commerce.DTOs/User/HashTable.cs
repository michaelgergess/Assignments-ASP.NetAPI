using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DTOs.User
{
    public static class HashTable
    {
       
            public static string HashPassword(string password)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
            }

            public static bool VerifyPassword(string hashedPassword, string enteredPassword)
            {
                string enteredPasswordHash = HashPassword(enteredPassword);
                return hashedPassword == enteredPasswordHash;
            }
        
    }
}
