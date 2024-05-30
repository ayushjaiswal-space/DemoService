using Space.Service.Common.Persistence;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application.Repositories;

public interface IRepositoryBase<TEntity, TEntityId>
    where TEntity : IEntityBase<TEntityId>
{
    Task<List<TEntity>> GetAll(bool asNoTracking = true);
    Task<TEntity> GetById(TEntityId id, bool asNoTracking = true, bool ensureNotNull = true);
    Task<TEntityId> Add(TEntity entity);
    Task Update(TEntity entity, bool beginTracking = false);
    Task Delete(TEntityId id);
}
