using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristiansOeCsProject.Repositories
{
    public interface ICRUDRepo<T>
    {
        Task<T> Create(T t);
        IAsyncEnumerable<T> ReadAll();
        Task<T> ReadById(string id);

        Task<T> Update(T t);

        void Delete(string id);
    }
}