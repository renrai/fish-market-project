using FishMarketProjectDomain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Cryptography
{
    public class Cryptography
    {
        public static string HashPassword(string password, int saltSize = 16, int iterations = 10000, int hashSize = 32)
        {
            byte[] salt = new byte[saltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(hashSize);
                byte[] combinedBytes = new byte[salt.Length + hash.Length];
                Array.Copy(salt, 0, combinedBytes, 0, salt.Length);
                Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);

                return Convert.ToBase64String(combinedBytes);
            }
        }
    }
}
