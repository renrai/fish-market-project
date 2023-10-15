using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Utils
{
    public class StringUtils
    {
        public static string RandomString(int length)
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[rand.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
    }
}
