using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database.Repositories.IRepositories
{
    public interface IRepositoryBase<MODEL> where MODEL : class
    {
        Task Add(MODEL model);
        void Update(MODEL model);
        void Remove(MODEL model);
        Task<MODEL> GetById(Guid id);
        Task<IEnumerable<MODEL>> GetAll();

    }
}
