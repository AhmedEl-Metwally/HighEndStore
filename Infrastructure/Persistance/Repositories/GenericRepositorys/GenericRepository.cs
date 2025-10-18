using Domain.Contracts.Specifications;

namespace Persistance.Repositories.GenericRepositorys
{
    public class GenericRepository<TEntity,TKey>(HighEndStoreDbContext _context) : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
               => await _context.Set<TEntity>().AddAsync(entity); 

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
               => asNoTracking ? await _context.Set<TEntity>().AsNoTracking().ToListAsync() : await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id)
               => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
                => _context.Set<TEntity>().Update(entity);  

        public void Delete(TEntity entity)
                => _context.Set<TEntity>().Remove(entity);


        //Specifications

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications)
                => await SpecificationEvluator.CreateQuery(_context.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
                => await SpecificationEvluator.CreateQuery(_context.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
                 => await SpecificationEvluator.CreateQuery(_context.Set<TEntity>(),specifications).CountAsync();
    }
}
