using System;
namespace spendwise.DataAccess.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity?> FindByIdAsync(string id);
		Task<TEntity> PostAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TEntity entity);
		Task DeleteAsync(string id);

	}
}

