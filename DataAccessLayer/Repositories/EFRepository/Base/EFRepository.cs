using DataAccessLayer.AppDataContext;
using DataAccessLayer.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.EFRepository.Base
{
    public class EFRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity: BaseEntity
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EFRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<long> CreateAsync(TEntity item)
        {
            var res = await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return res.Entity.Id;
        }

        public async Task<TEntity> GetById(long id)
        {
            var result = await _dbSet.FindAsync(id);

            if (result is null)
            {
                return null;
            }

            return result;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable<TEntity>();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateAsync(TEntity item)
        {
            _dbSet.Update(item);
            await SaveChangesAsync();
        }
    }
}
