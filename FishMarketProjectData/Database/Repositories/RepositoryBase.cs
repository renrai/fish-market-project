using AutoMapper;
using FishMarketProjectData.Database.Entities;
using FishMarketProjectData.Database.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database.Repositories
{
    public class RepositoryBase<MODEL, ENTITY> : IRepositoryBase<MODEL>
        where MODEL : class
        where ENTITY : BaseEntity
    {
        #region Fields

        protected readonly FishMarketContextDb _db;
        protected readonly IMapper _mapper;

        #endregion

        public RepositoryBase(FishMarketContextDb context)
        {
            _db = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(Mapping.MappingProfile));

            });
            _mapper = config.CreateMapper();
        }

        #region Public Methods

        public void Add(MODEL model)
        {
            var entity = _mapper.Map<ENTITY>(model);
            _db.Entry(entity).State = EntityState.Added;
        }

        public void Update(MODEL model)
        {
            var entity = _mapper.Map<ENTITY>(model);
            _db.Entry(entity).State = EntityState.Modified;

        }

        public void Remove(MODEL model)
        {
            var entity = _mapper.Map<ENTITY>(model);
            _db.Entry(entity).State = EntityState.Deleted;

        }
        public async Task<MODEL> GetById(Guid id)
        {
            var entity = await _db.Set<ENTITY>().FindAsync(id);
            if (entity != null)
            {
                _db.Entry(entity).State = EntityState.Detached;
            }

            return _mapper.Map<MODEL>(entity);
        }
        public async Task<IEnumerable<MODEL>> GetAll()
        {
            var entities = await _db.Set<ENTITY>().ToListAsync();

            return _mapper.Map<List<MODEL>>(entities);
        }
        #endregion
    }
}
