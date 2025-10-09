using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Service.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : Base<TKey>
    {
        public BaseSpecification(Expression<Func<TEntity,bool>> expression)
        {
            Criteria=expression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    }
}
