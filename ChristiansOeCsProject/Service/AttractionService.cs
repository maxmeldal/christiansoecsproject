using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    
    /**
     * Service klasser fungerer som mellemmand til repositories for at
     * opnå lav kobling, samt skalerbarhed.
     * Rent praktisk gør de ikke mere end at hente Task objekter fra
     * repositories og konvertere dem til
     * de rigtige objekter, så de kan bruges
     */
    public class AttractionService
    {
        private AttractionRepo repo = new AttractionRepo();

        public Task<Attraction> Create(Attraction attraction)
        {
           return repo.Create(attraction);
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