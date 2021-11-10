using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    public class AttractionService
    {
        private AttractionRepo repo = new AttractionRepo();

        public async void Create(Attraction attraction)
        {
            
        }

        public async Task<List<Attraction>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }

        public Attraction ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }

        public async Task<Attraction> Update(Attraction attraction)
        {
            return null;
        }

        public async void Delete(string id)
        {
            
        }
    }
}