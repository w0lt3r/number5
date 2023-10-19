using System.Linq.Expressions;

namespace Number5Poc.Data;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Insert(TEntity entity);
    void Update(TEntity entityToUpdate);

    Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");
}