using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                // if there is a where filter execute the below as well
                query = query.Where(spec.Criteria);  // p => p.ProductTypeId == id
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            // here aggregate is because here we are combining all of the "includes"
            // here "current" is the entity we are passing in.
            // here "include" is the expresssion of the include statement.
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;

        }

    }
}