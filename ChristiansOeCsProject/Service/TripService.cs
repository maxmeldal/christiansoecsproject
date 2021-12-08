using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;
using Microsoft.AspNetCore.Routing;

namespace ChristiansOeCsProject.Service
{
    //Objekter fra TripRepo bliver kaldt i Service klassen.

    public class TripService
    {
        private TripRepo repo = new TripRepo();
        //Objekter fra repo sættes i create og herefter bliver retur værdien bliver kaldt af TripController og bruges til POST / create

        public Task<Trip> Create(Trip trip)
        {
            return repo.Create(trip);
        }
        //Objekter fra repo sættes i readAll og bliver herefter kaldt af TripController
        public async Task<List<Trip>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }
        //ReadById metode henter id fra et Facility objekt og kaldes derefter af TripController og bruges til Get  funk
        public Trip ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
        //Update metode henter id fra et Facility objekt og kaldes derefter af TripController og bruges til PUT / Updatefunk
        public Task<Trip> Update(Trip trip)
        {
            return repo.Update(trip);
        }
        //ReadById metode henter id fra et Facility objekt og kaldes derefter af TripController og bruges til DELETE funk
        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}