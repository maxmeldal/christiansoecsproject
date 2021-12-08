using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristiansOeCsProject.Repositories
{
    /**
     * Interface til de repositories der skal oprettes.
     * Interfaces tager et generisk objekt og bruger det i de forskellige metoder (med undtagelse for delete).
     * Dette interface skal implementeres af alle repositories for at sikre at de rette metoder bliver implementeret.
     * På denne måde kan man opnå lav kobling idet vi kan have service klasser tilknyttet hver repository,
     * og kan udskifte databaserne uafhængigt af resten af applikationen
     */
    public interface ICRUDRepo<T>
    {
        Task<T> Create(T t);
        IAsyncEnumerable<T> ReadAll();
        Task<T> ReadById(string id);

        Task<T> Update(T t);

        void Delete(string id);
    }
}