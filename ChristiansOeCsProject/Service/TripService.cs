using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;
using Microsoft.AspNetCore.Routing;

namespace ChristiansOeCsProject.Service
{
    public class TripService
    {
        private TripRepo repo = new TripRepo();

        public async void Create(Trip trip)
        {
            repo.Create(trip);
        }

        public async Task<List<Trip>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }

        public Trip ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }

        public async Task<Trip> Update(Trip trip)
        {
            return await repo.Update(trip);
        }

        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}