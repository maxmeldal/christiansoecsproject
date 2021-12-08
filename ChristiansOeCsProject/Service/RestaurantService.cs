using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    //Objekter fra Restaurant bliver kaldt i Service klassen.

    public class RestaurantService
    {
        private RestaurantRepo repo = new RestaurantRepo();

        //Objekter fra repo sættes i create og herefter bliver retur værdien kaldt af RestaurantController og bruges til POST / create

        public Task<Restaurant> Create(Restaurant restaurant)
        {
            return repo.Create(restaurant);
        }
        //Objekter fra repo sættes i readAll og bliver herefter kaldt af RestaurantController

        public async Task<List<Restaurant>> ReadAll()
        {
            
            return await repo.ReadAll().ToListAsync();
        }

        //ReadById metode henter id fra et Facility objekt og kaldes derefter af RestaurantController og bruges til Get  funk

        public Restaurant ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
        //Update metode henter id fra et Facility objekt og kaldes derefter af RestaurantController og bruges til PUT / Updatefunk

        public Task<Restaurant> Update(Restaurant restaurant)
        {
            return repo.Update(restaurant);
        }
        //ReadById metode henter id fra et Restaurant objekt og kaldes derefter af AttractionController og bruges til DELETE funk

        public void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}