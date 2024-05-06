namespace BulkyWeb.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }

}
