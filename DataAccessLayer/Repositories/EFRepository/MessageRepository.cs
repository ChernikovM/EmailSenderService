using DataAccessLayer.AppDataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.EFRepository.Base;
using DataAccessLayer.Repositories.EFRepository.Interfaces;
using HotChocolate;

namespace DataAccessLayer.Repositories.EFRepository
{
    public class MessageRepository : EFRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {

        }
    }
}
