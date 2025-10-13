

using System.Collections.Concurrent;

namespace Persistance.Repositories.UnitOfWorks
{
    public class UnitOfWork(HighEndStoreDbContext _context) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _dictionary = new();

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
            => (IGenericRepository<TEntity, Tkey>)_dictionary.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, Tkey>(_context));

        public async Task<int> SaveChangesAsync()
                => await _context.SaveChangesAsync();
    }
}
