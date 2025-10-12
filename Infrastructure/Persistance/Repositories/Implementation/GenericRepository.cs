
namespace Persistance.Repositories.Implementation
{
    public class GenericRepository<TEntity,TKey>(HighEndStoreDbContext _context) : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
               => await _context.Set<TEntity>().AddAsync(entity); 

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
               => asNoTracking ? await _context.Set<TEntity>().AsNoTracking().ToListAsync() : await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetAllByIdAsync(TKey id)
               => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
                => _context.Set<TEntity>().Update(entity);  

        public void Delete(TEntity entity)
                => _context.Set<TEntity>().Remove(entity);
    }
}
