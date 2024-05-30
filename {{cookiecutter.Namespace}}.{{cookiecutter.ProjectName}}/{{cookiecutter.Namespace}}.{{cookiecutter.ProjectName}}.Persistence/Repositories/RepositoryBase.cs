using Microsoft.EntityFrameworkCore;
using Space.Service.Common.Exceptions;
using Space.Service.Common.Persistence;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence.Repositories;

[ExcludeFromCodeCoverage]
public class RepositoryBase<TEntity, TEntityId> : IRepositoryBase<TEntity, TEntityId>
    where TEntity : class, IEntityBase<TEntityId>
{
    protected readonly {{cookiecutter.ProjectName}}DbContext db;

    public RepositoryBase({{cookiecutter.ProjectName}}DbContext db)
    {
        this.db = db;
    }

    public virtual async Task<List<TEntity>> GetAll(bool asNoTracking = true)
    {
        IQueryable<TEntity> query = db.Set<TEntity>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetById(TEntityId id, bool asNoTracking = true, bool ensureNotNull = true)
    {
        IQueryable<TEntity> query = db.Set<TEntity>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        TEntity entity = await query.FirstOrDefaultAsync(entity => entity.Id.Equals(id));

        if (ensureNotNull)
        {
            entity.EnsureNotNull(id);
        }

        return entity;
    }

    public virtual async Task<TEntityId> Add(TEntity entity)
    {
        await db.Set<TEntity>().AddAsync(entity);
        await db.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task Update(TEntity entity, bool beginTracking = false)
    {
        // beginTracking should be true only in a "Disconnected Scenario":
        // https://www.learnentityframeworkcore.com/dbcontext/modifying-data#disconnected-scenario
        if (beginTracking)
        {
            db.Set<TEntity>().Update(entity);
        }

        await db.SaveChangesAsync();
    }

    public virtual async Task Delete(TEntityId id)
    {
        TEntity entity = await db.Set<TEntity>().FindAsync(id);
        entity.EnsureNotNull(id);
        db.Remove(entity);
        await db.SaveChangesAsync();
    }
}
