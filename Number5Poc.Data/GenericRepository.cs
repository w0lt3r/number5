using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Number5Poc.Data;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    internal Context context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(Context context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task Insert(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}