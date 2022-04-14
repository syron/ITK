namespace KIT.Services;

public interface IBaseService<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T obj);
    Task<T> UpdateAsync(T obj);
    Task DeleteAsync(T obj);
}