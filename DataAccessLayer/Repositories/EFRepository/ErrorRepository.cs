using DataAccessLayer.AppDataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.EFRepository.Base;
using DataAccessLayer.Repositories.EFRepository.Interfaces;

namespace DataAccessLayer.Repositories.EFRepository
{
    public class ErrorRepository : EFRepository<Error>, IErrorRepository
    {
        public ErrorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
