using FishMarketProjectData.Database.Repositories.IRepositories;
using FishMarketProjectDomain.IService;
using FishMarketProjectDomain.Models;
using FishMarketProjectDomain.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectService.Services
{
    public class FishService : IFishService
    {
        private static IUnitOfWork _unitOfWork;

        public FishService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Delete(Guid id)
        {
            var fishToDelete = await _unitOfWork.FishRepository.GetById(id);
            if (fishToDelete is null)
                return false;

            _unitOfWork.FishRepository.Remove(fishToDelete);
            _unitOfWork.Commit();

            return true;
        }

        public async Task<List<Fish>> Get()
        {
            var fishs = await _unitOfWork.FishRepository.GetAll();
            return fishs.ToList();
        }

        public async Task<Fish> GetById(Guid id)
        {
            return await _unitOfWork.FishRepository.GetById(id);
        }

        public async Task<Fish> Post(FishRequest request)
        {
            Fish fish = new Fish();
            fish.Specie = request.Specie;
            fish.Price = request.Price;
            await _unitOfWork.FishRepository.Add(fish);
            _unitOfWork.Commit();
            return fish;
        }

        public async Task<Fish> Put(FishRequest request)
        {
            var fish = await _unitOfWork.FishRepository.GetById(request.Id);
            if (fish is null)
                return null;

            fish.UpdateDate = DateTime.Now;
            fish.Specie = request.Specie;
            fish.Price = request.Price;
            _unitOfWork.FishRepository.Update(fish);
            _unitOfWork.Commit();
            return fish;
        }

        public async Task<bool> UploadPhoto(IFormFile file, Guid id)
        {
            var fishDb = await _unitOfWork.FishRepository.GetById(id);
            if (file != null && file.Length > 0 && fishDb != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    fishDb.Photo = imageData;
                    fishDb.UpdateDate = DateTime.Now;
                    _unitOfWork.FishRepository.Update(fishDb);
                    _unitOfWork.Commit();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
