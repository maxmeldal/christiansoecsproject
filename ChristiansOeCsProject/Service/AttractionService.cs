using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    
    
    public class AttractionService
    {
        //Objekter fra AttractionRepo bliver kaldt i Service klassen.
        private AttractionRepo repo = new AttractionRepo();

        
        //Objekter fra repo sættes i create og herefter bliver retur værdien bliver kaldt af AttractionController og bruges til POST / create
        public Task<Attraction> Create(Attraction attraction)
        {
           return repo.Create(attraction);
        }
        //Objekter fra repo sættes i readAll og bliver herefter kaldt af AttractionController
        public async Task<List<Attraction>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }
        //ReadById metode henter id fra et Attraction objekt og kaldes derefter af AttractionController og bruges til Get / delete funk
        public Attraction ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
        //Update metode henter id fra et Attraction objekt og kaldes derefter af AttractionController og bruges til PUT / Updatefunk
        public Task<Attraction> Update(Attraction attraction)
        {
            return repo.Update(attraction);
        }
        //ReadById metode henter id fra et Attraction objekt og kaldes derefter af AttractionController og bruges til Get / delete funk
        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}