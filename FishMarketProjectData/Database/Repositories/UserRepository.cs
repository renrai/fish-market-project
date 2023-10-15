using FishMarketProjectData.Database.Entities;
using FishMarketProjectData.Database.Repositories.IRepositories;
using FishMarketProjectDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database.Repositories
{
    public class UserRepository : RepositoryBase<User, UserEntity>, IUserRepository
    {
        private readonly FishMarketContextDb _context;

        public UserRepository(FishMarketContextDb context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfEmailExistsDB(string email)
        {
            var emailRecordDb = await _context.Users.FirstOrDefaultAsync(a => a.Email.Equals(email));
            if(emailRecordDb == null) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var userDb =  await _context.Users.FirstOrDefaultAsync(a => a.Email.Equals(email));
            return _mapper.Map<User>(userDb);
        }
    }
}
