using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.AppDataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Mail> Mails { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Error> Errors { get; set; }
    }
}
