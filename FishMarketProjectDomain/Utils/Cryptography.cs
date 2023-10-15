using FishMarketProjectDomain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Utils
{
    public class Cryptography
    {
        public static string StringToBase64(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }
    }
}
