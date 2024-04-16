using System;
using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess.Repositories
{
	public class ProductRepository : BaseRepository<Product>
	{
		public ProductRepository(SpendWiseContext context): base(context)
		{
		}

		public override async Task<IEnumerable<Product>> GetAllAsync()
		{
            try
            {
                return await _context.Products.Include(c => c.Categories).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving data from DB: {ex.Message}", ex);
            }
        }

        public override async Task<Product?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Products.Include(c => c.Categories).SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving data from DB: {ex.Message}", ex);
            }
        }
    }
}

