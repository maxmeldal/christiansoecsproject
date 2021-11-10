using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    public class RestaurantService
    {
        private RestaurantRepo repo = new RestaurantRepo();

        public async void Create(Restaurant restaurant)
        {
            
        }

        public async Task<List<Restaurant>> ReadAll()
        {
            
            return await repo.ReadAll().ToListAsync();
        }

        public Restaurant ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }

        public async Task<Restaurant> Update(Restaurant restaurant)
        {
            return null;
        }

        public async void Delete(string id)
        {
            
        }
    }
}