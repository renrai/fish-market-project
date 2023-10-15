using FishMarketProjectDomain.Models;
using FishMarketProjectDomain.Models.Request;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.IService
{
    public interface IFishService
    {
        Task<Fish> Post(FishRequest request);
        Task<Fish> Put(FishRequest request);
        Task<List<Fish>> Get();
        Task<Fish> GetById(Guid id);
        Task<bool> UploadPhoto(IFormFile file, Guid id);

        Task<bool> Delete(Guid id);
    }
}
