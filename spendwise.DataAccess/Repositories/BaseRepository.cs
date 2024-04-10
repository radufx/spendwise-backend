using System;

using Microsoft.EntityFrameworkCore;

namespace spendwise.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SpendWiseContext _context;

        public BaseRepository(SpendWiseContext context)
        {
            _context = context;
        }

        public virtual async Task DeleteAsync(string id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);

                if (entity == null)
                {
                    return;
                }

                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when deleting entity by id {id}, {ex.Message}", ex);
            }
        }

        public virtual async Task<TEntity?> FindByIdAsync(string id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving entity by id {id}, {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch(Exception ex)
            { 
                throw new Exception($"Error when retrieving data from DB: {ex.Message}", ex);
            }
        }

        public virtual async Task<TEntity> PostAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when adding data to DB: {ex.Message}", ex);
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when updating data in DB: {ex.Message}", ex);
            }
        }
    }
}

