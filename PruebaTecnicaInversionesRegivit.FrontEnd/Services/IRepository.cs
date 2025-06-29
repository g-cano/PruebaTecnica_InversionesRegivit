namespace PruebaTecnicaInversionesRegivit.FrontEnd.Services
{
    public interface IRepository
    {
        Task<T> GetAsync<T>(string url);
        Task<object> PostAsync<T>(string url, T entity);
        Task<object> PostByIdAsync<T>(string url, int id, T entity);
        Task<object> PutAsync<T>(string url, T entity);
        Task<T> GetByIdAsync<T>(string url, int id);
        Task<object> DeleteByIdAsync<T>(string url, int id);
    }
}
