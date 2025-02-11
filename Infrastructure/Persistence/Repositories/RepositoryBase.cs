using Microsoft.EntityFrameworkCore;
using PlacasAPI.Infrastructure.Persistence.Context;
using PlacasAPI.Interfaces.Respositories;

namespace PlacasAPI.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly PlateApiContext _context;

        public RepositoryBase(PlateApiContext context)
        {
            _context = context;
        }
        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
