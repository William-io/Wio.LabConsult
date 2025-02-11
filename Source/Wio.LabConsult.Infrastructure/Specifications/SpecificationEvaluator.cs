using Microsoft.EntityFrameworkCore;
using Wio.LabConsult.Application.Specifications;

namespace Wio.LabConsult.Infrastructure.Specifications;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        
        if (specification.Criterion != null)
        {
            inputQuery = inputQuery.Where(specification.Criterion);
        }
        if (specification.OrderBy != null)
        {
            inputQuery = inputQuery.OrderBy(specification.OrderBy);
        }
        if (specification.OrderByDescending != null)
        {
            inputQuery = inputQuery.OrderByDescending(specification.OrderByDescending);
        }
        if (specification.IsPagingEnabled)
        {
            inputQuery = inputQuery.Skip(specification.Skip).Take(specification.Take);
        }

        inputQuery = specification.Includes!.Aggregate(inputQuery, (current, include) => current.Include(include)).AsSplitQuery().AsNoTracking();

        return inputQuery;
    }
}
