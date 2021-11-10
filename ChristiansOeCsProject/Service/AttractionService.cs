using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    public class AttractionService
    {
        private AttractionRepo repo = new AttractionRepo();

        public async void Create(Attraction attraction)
        {
            repo.Create(attraction);
        }

        public async Task<List<Attraction>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }

        public Attraction ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }

        public Task<Attraction> Update(Attraction attraction)
        {
            return repo.Update(attraction);
        }

        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}