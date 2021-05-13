using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.EFRepository.Interfaces;
using HotChocolate;
using System.Threading.Tasks;

namespace EmailSenderService.GraphQL
{
    public class Query
    {
        public async Task<Mail> GetMail(long id,
            [Service] IMailRepository mailRepository)
        {
            return await mailRepository.GetById(id);
        }
    }
}
