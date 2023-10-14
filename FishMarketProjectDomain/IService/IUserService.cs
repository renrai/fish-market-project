using FishMarketProjectDomain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.IService
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegisterRequest user);
    }
}
