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
        private static IEmailSenderService _emailSenderService;
        public UserService(IUnitOfWork unitOfWork, IEmailSenderService emailSenderService)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
        }
        public async Task<bool> RegisterUser(UserRegisterRequest user)
        {
            var emailAlreadyExist = await _unitOfWork.UserRepository.CheckIfEmailExistsDB(user.Email);

            if (emailAlreadyExist)
                throw new ArgumentException("Email Already Exists! Go to Login");

            var newUser = new User { Password = Cryptography.HashPassword(user.Password), Email = user.Email, TokenVerification = Guid.NewGuid()};

            _unitOfWork.UserRepository.Add(newUser);

            _unitOfWork.Commit();

            await _emailSenderService.SendEmailAsync(newUser.Email, newUser.TokenVerification);

            return true;
        }
    }
}
