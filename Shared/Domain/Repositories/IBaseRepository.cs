namespace Collectioneer.API.Shared.Domain.Repositories
{
		public interface IBaseRepository<T> where T : class
		{
				Task<IEnumerable<T>> GetAll();
				Task<T?> GetById(int id);
				Task<T?> GetByName(string name); //New Code added 11-09-2024
				Task<T> Add(T entity);
				Task<T> Update(T entity);
				Task Delete(int id);
		}
}

