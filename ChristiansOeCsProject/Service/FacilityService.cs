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
    public class FacilityService
    {
        private FacilityRepo repo = new FacilityRepo();

        public Task<Facility> Create(Facility facility)
        {
            return repo.Create(facility);
        }

        public async Task<List<Facility>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }
        public Facility ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
        public Task<Facility> Update(Facility facility)
        {
            return repo.Update(facility);
        }
        public async void Delete(string id)
        {
            repo.Delete(id);
        }
    }
}