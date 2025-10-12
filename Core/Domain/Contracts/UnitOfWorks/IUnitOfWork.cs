
using Domain.Contracts.GenericRepositorys;

namespace Domain.Contracts.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity, Tkey> GenericRepository<TEntity,Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
