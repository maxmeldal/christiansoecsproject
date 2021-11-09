using System.Collections.Generic;

namespace ChristiansOeCsProject.Repositories
{
    public interface ICRUDRepo<T>
    {
        IAsyncEnumerable<T> ReadAll();
        T ReadById(int id);

    }
}