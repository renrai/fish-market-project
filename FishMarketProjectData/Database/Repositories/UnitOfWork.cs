using FishMarketProjectData.Database.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly FishMarketContextDb _context;
        private IUserRepository _userRepository;
        private IFishRepository _fishRepository;

        public UnitOfWork(FishMarketContextDb context)
        {
            _context = context;
        }
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));
        public IFishRepository FishRepository => _fishRepository ?? (_fishRepository = new FishRepository(_context));

        public int Commit()
        {
            var n = _context.SaveChanges();
            DetachAllEntities();

            return n;
        }

        private void DetachAllEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
