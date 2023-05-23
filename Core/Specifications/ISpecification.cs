using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // This would be our "Where" criteria is
        // Expression<Func<inputType, returnType>> Criteria { get; }
        Expression<Func<T, bool>> Criteria { get; }

        // List of includes.
        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }


    }
}