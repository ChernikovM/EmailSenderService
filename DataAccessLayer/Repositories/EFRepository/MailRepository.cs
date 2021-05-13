using DataAccessLayer.AppDataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.EFRepository.Base;
using DataAccessLayer.Repositories.EFRepository.Interfaces;
using HotChocolate;

namespace DataAccessLayer.Repositories.EFRepository
{
    public class MailRepository : EFRepository<Mail>, IMailRepository
    {
        public MailRepository(AppDbContext context) : base(context)
        {

        }
    }
}
