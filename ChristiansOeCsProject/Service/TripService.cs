using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;
using Microsoft.AspNetCore.Routing;

namespace ChristiansOeCsProject.Service
{
    /**
     * Service klasser fungerer som mellemmand til repositories for at
     * opnå lav kobling, samt skalerbarhed.
     * Rent praktisk gør de ikke mere end at hente Task objekter fra
     * repositories og konvertere dem til
     * de rigtige objekter, så de kan bruges
     */
    public class TripService
    {
        private TripRepo repo = new TripRepo();

        public Task<Trip> Create(Trip trip)
        {
            return repo.Create(trip);
        }
        public async Task<List<Trip>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }
        public Trip ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
        public Task<Trip> Update(Trip trip)
        {
            return repo.Update(trip);
        }
        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}