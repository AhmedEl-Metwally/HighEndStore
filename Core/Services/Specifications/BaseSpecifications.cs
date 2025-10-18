using Domain.Contracts.Specifications;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        //Criteria
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }


        //Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression ) => IncludeExpressions.Add( includeExpression);


        //Sotring
        public Expression<Func<TEntity, object>> orderBy { get; private set; }

        public Expression<Func<TEntity, object>> orderDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => orderBy = orderByExpression;
        protected void AddOrderDescending(Expression<Func<TEntity, object>> orderDescendingExpression) => orderDescending = orderDescendingExpression;




    }
}


