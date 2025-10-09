using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : Base<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity,TKey> specification);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
    }
}
