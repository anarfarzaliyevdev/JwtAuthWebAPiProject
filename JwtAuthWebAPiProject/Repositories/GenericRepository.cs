using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JwtAuthWebAPiProject.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            if (entity != null)
            {
                await _dbSet.AddAsync(entity);
                await _appDbContext.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<T> DeleteAsync(int entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (_appDbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            TrySetProperty(entity, "IsDeleted", true);
            var result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return entity;
            }
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var data = await _dbSet.FindAsync(id);
            return data;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
            var result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        private bool TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
                return true;
            }
            return false;
        }
    }
}
