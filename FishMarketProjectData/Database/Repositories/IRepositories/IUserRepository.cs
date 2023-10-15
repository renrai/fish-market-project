using FishMarketProjectDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database.Repositories.IRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<bool> CheckIfEmailExistsDB(string email);
        Task<User> GetUserByEmail(string email);

    }
}
