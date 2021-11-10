using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    public class FacilityService
    {
        private FacilityRepo repo = new FacilityRepo();

        public async void Create(Facility facility)
        {
            repo.Create(facility);
        }

        public async Task<List<Facility>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }

        public Facility ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }

        public async Task<Facility> Update(Facility facility)
        {
            return await repo.Update(facility);
        }

        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}