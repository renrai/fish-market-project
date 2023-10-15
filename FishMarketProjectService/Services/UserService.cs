using FishMarketProjectData.Database.Repositories.IRepositories;
using FishMarketProjectDomain.IService;
using FishMarketProjectDomain.Models;
using FishMarketProjectDomain.Models.Request;
using FishMarketProjectDomain.Utils;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IEmailSenderService emailSenderService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
        }

        public async Task<bool> Login(UserRequest user)
        {
            var userDb = await _unitOfWork.UserRepository.GetUserByEmail(user.Email);
            if (userDb == null || userDb?.Password != Cryptography.StringToBase64(user.Password) || userDb.EmailVerified == false) { return false; }
           
            return true;
        }

        public async Task<bool> RegisterUser(UserRequest user)
        {
            var emailAlreadyExist = await _unitOfWork.UserRepository.CheckIfEmailExistsDB(user.Email);

            if (emailAlreadyExist)
                return false;

            var newUser = new User { Password = Cryptography.StringToBase64(user.Password), Email = user.Email, TokenVerification = StringUtils.RandomString(6)};

            await _unitOfWork.UserRepository.Add(newUser);

            _unitOfWork.Commit();

            await _emailSenderService.SendEmailAsync(newUser.Email, newUser.TokenVerification);

            return true;
        }

        public async Task<bool> ResendEmailVerification(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null || user.EmailVerified is true)
                return false;

            await _emailSenderService.SendEmailAsync(user.Email, user.TokenVerification);

            return true;
        }

        public async Task<bool> ValidateToken(string email, string token)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null || user.TokenVerification != token)
                return false;

            user.EmailVerified = true;

            _unitOfWork.UserRepository.Update(user);

            _unitOfWork.Commit();

            return true;

        }
    }
}
