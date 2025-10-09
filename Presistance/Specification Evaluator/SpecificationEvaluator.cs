using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Presistance.Specification_Evaluator
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputquery, ISpecification<TEntity, TKey> specification) where TEntity : Base<TKey>
        {
            var Query = inputquery;
            if(specification.Criteria is not null)
            {
                Query = Query.Where(specification.Criteria);
            }
            return Query;

        }
    }
}
