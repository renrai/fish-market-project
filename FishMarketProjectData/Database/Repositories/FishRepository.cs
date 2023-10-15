using FishMarketProjectData.Database.Entities;
using FishMarketProjectData.Database.Repositories.IRepositories;
using FishMarketProjectDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database.Repositories
{
    public class FishRepository : RepositoryBase<Fish, FishEntity>, IFishRepository
    {
        private readonly FishMarketContextDb _context;

        public FishRepository(FishMarketContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
