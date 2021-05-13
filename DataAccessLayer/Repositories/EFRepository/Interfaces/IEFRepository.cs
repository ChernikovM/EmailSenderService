using DataAccessLayer.Entities.Base;

namespace DataAccessLayer.Repositories.EFRepository.Interfaces
{
    public interface IEFRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
    }
}
