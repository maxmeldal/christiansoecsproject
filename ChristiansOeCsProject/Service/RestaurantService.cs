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
    public class RestaurantService
    {
        private RestaurantRepo repo = new RestaurantRepo();
        
        public Task<Restaurant> Create(Restaurant restaurant)
        {
            return repo.Create(restaurant);
        }

        public async Task<List<Restaurant>> ReadAll()
        {
            
            return await repo.ReadAll().ToListAsync();
        }
        
        public Restaurant ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }

        public Task<Restaurant> Update(Restaurant restaurant)
        {
            return repo.Update(restaurant);
        }

        public void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}