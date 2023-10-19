namespace Number5Poc.Data;

public interface IUnitOfWork : IDisposable
{
    Task SaveChanges();
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}