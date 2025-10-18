using Domain.Contracts.Specifications;

namespace Persistance
{
    public static class SpecificationEvluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>
                                                                    (
                                                                        IQueryable<TEntity> inputQuery,
                                                                        ISpecifications<TEntity,TKey> specifications
                                                                    ) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);


            if (specifications.orderBy is not null)
                query = query.OrderBy(specifications.orderBy);
            if(specifications.orderDescending is not null)
                query = query.OrderByDescending(specifications.orderDescending);

       
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count>0)
                query = specifications.IncludeExpressions.Aggregate(query,(currentQuery,expression) => currentQuery.Include(expression));



            return query;
        }
    }
}
