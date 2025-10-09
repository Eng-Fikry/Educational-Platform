using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity : Base<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
    }
}
