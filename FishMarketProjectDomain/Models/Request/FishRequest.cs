using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Models.Request
{
    public class FishRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public required string Specie { get; set; }
        public required float Price { get; set; }
    }
}
