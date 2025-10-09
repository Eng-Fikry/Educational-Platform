using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Reposatories
{
    public class UnitOfWork(PlatformDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Base<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;
            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories[typeName] = Repo;
                return Repo;
            }
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
