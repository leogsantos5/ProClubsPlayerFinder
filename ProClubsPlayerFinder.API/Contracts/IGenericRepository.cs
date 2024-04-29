namespace ProClubsPlayerFinder.API.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);

        Task<T> GetAsync(string? id);

        Task<List<T>> GetAllAsync();

        Task<bool> Exists(int id);

        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(List<T> entities);
    }
}
