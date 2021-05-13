using DataAccessLayer.Entities.Base;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<long> CreateAsync(TEntity item);

        Task<TEntity> GetById(long id);

        Task UpdateAsync(TEntity item);

        Task SaveChangesAsync();
    }
}
