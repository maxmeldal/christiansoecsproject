﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;

namespace ChristiansOeCsProject.Service
{
    public class AttractionService
    {
        private AttractionRepo repo = new AttractionRepo();
        public async Task<List<Attraction>> ReadAll()
        {
            return await repo.ReadAll().ToListAsync();
        }

        public Attraction ReadById(string id)
        {
            return repo.ReadById(id).Result;
        }
    }
}