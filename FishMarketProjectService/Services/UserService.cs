using FishMarketProjectData.Database.Repositories.IRepositories;
using FishMarketProjectDomain.Cryptography;
using FishMarketProjectDomain.IService;
using FishMarketProjectDomain.Models;
using FishMarketProjectDomain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectService.Services
{
    public class UserService : IUserService
    {
        private static IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> RegisterUser(UserRegisterRequest user)
        {
            var emailAlreadyExist = await _unitOfWork.UserRepository.CheckIfEmailExistsDB(user.Email);

            if (emailAlreadyExist)
                throw new ArgumentException("Email Already Exists! Go to Login");

            var newUser = new User { Password = Cryptography.HashPassword(user.Password), Email = user.Email};

            _unitOfWork.UserRepository.Add(newUser);
            _unitOfWork.Commit();
            return true;
        }
    }
}
