namespace Domain.Contracts.GenericRepositorys
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey> 
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        Task<TEntity?> GetAllByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
