using Common;
using Infrastructure.GenericModel;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly QuangDbContext _context;
        private DbSet<T> _dbset;
        public GenericRepository(QuangDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }
        public async Task<int> AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            entity.CreatedAt = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _dbset.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return ConstantsStatus.Failed;
            entity.DeletedAt = DateTime.Now;
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbset.Update(entity);
            entity.UpdatedAt = DateTime.Now;
            return await _context.SaveChangesAsync();
        }
    }
}
