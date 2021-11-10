using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristiansOeCsProject.Repositories
{
    public interface ICRUDRepo<T>
    {
        void Create(T t);
        IAsyncEnumerable<T> ReadAll();
        Task<T> ReadById(string id);

        Task<T> Update(T t);

        void Delete(string id);
    }
}