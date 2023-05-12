namespace ContosoUniversity.IGenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
      public  IQueryable<TEntity> GetAll();
      public Task<TEntity> GetById(int id);
      public TEntity Delete(TEntity entity);
      public TEntity Insert(TEntity entity);
      public TEntity Update(TEntity entity);
    }
}
