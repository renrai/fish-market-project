using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Models.Request
{
    public class UserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
