﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class FacilityRepo : ICRUDRepo<Facility>
    {

        private FirestoreDb db = FirebaseConnection.GetConnection();

        public void Create(Facility t)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<Facility> ReadAll()
        {

            var qref = db.Collection("facilities");
            var snap = await qref.GetSnapshotAsync();

            foreach (var docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    var id = docsnap.Id;
                    
                    var dict = docsnap.ToDictionary();
                    var lat = Convert.ToDouble(dict["lat"]);
                    var longi = Convert.ToDouble(dict["long"]);
                    var name = Convert.ToString(dict["name"]);
                    
                    yield return new Facility(id, lat, longi, name);
                }
            }
        }

        public async Task<Facility> ReadById(string id)
        {
            var DocRef = db.Collection("facilities").Document(id);
            var docsnap = await DocRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                var dict = docsnap.ToDictionary();
                var lat = Convert.ToDouble(dict["lat"]);
                var longi = Convert.ToDouble(dict["long"]);
                var name = Convert.ToString(dict["name"]);
                return new Facility(id, lat, longi, name);
            }

            return null;
        }

        public Task<Facility> Update(Facility t)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}