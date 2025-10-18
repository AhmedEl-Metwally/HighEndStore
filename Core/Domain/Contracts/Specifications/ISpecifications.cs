using System.Linq.Expressions;

namespace Domain.Contracts.Specifications
{
    public interface ISpecifications<TEntity,TKey> where TEntity:BaseEntity<TKey> 
    {
        public Expression<Func<TEntity,bool>> Criteria { get;  }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get;}

        public Expression<Func<TEntity,object>> orderBy { get; }
        public Expression<Func<TEntity,object>> orderDescending { get; }
    }
}
