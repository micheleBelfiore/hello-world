using ContosoUniversity.Data;
using ContosoUniversity.IGenericRepository;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal SchoolContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(SchoolContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public  TEntity Delete(TEntity entity)
        {
            if (entity == null) { throw new NotImplementedException($"entity is null"); }
            _dbSet.Remove(entity);            
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) { throw new NotImplementedException($"nothing entity find with this id {id}"); }
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            if (entity == null) { throw new NotImplementedException($"entity is  null"); }
            _dbSet.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null) { throw new NotImplementedException($"entity is  null"); }
            _dbSet.Update(entity);
            return entity;
        }

    }
}
