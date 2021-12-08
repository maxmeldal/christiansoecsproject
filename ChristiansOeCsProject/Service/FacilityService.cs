using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    //Objekter fra FacilityRepo bliver kaldt i Service klassen.

    public class FacilityService
    {
        private FacilityRepo repo = new FacilityRepo();
    
        //Objekter fra repo sættes i create og herefter bliver retur værdien bliver kaldt af FacilityController og bruges til POST / create

        public Task<Facility> Create(Facility facility)
        {
            return repo.Create(facility);
        }
        //Objekter fra repo sættes i readAll og bliver herefter kaldt af FacilityController

        public async Task<List<Facility>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }
        //ReadById metode henter id fra et Facility objekt og kaldes derefter af FacilityController og bruges til Get  funk
        public Facility ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
        //Update metode henter id fra et Facility objekt og kaldes derefter af FacilityController og bruges til PUT / Updatefunk
        public Task<Facility> Update(Facility facility)
        {
            return repo.Update(facility);
        }
        //ReadById metode henter id fra et Facility objekt og kaldes derefter af AttractionController og bruges til DELETE funk
        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}