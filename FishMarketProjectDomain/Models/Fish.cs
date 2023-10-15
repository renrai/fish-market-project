using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Models
{
    public class Fish : BaseModel
    {
        public string Specie { get; set; } = string.Empty;
        public float Price { get; set; }
        public byte[] Photo { get; set; } = new byte[0];
    }
}
