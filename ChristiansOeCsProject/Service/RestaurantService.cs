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
            repo.Create(restaurant);
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

        public async void Delete(string id)
        {
            
        }
    }
}