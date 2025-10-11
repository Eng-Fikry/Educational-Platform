using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Specification_Evaluator;

namespace Presistance.Reposatories
{
    public class GenericRepository<TEntity, TKey>(PlatformDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : Base<TKey>
    {
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id) ?? null!;

        

        public void Remove(TEntity entity) =>  _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            var Query = _dbContext.Set<TEntity>();
            return await SpecificationEvaluator.CreateQuery(Query, specification).FirstOrDefaultAsync();


        }

    }
}

