using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RideSharingApp.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<int?> Create(TEntity item);
        Task<int?> Update(TEntity item);
        Task<bool?> Delete(int id);
    }
}
