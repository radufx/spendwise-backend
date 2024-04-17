using System;
using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(SpendWiseContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories.Include(c => c.Products).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving data from DB: {ex.Message}", ex);
            }
        }

        public override async Task<Category?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Categories.AsNoTracking().Include(c => c.Products).SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving data from DB: {ex.Message}", ex);
            }
        }
    }
}

