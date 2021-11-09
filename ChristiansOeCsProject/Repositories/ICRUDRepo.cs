using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristiansOeCsProject.Repositories
{
    public interface ICRUDRepo<T>
    {
        IAsyncEnumerable<T> ReadAll();
        Task<T> ReadById(string id);

    }
}