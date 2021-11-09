using System;
using System.Collections.Generic;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class FacilityRepo : ICRUDRepo<Facility>
    {

        private FirestoreDb db = FirebaseConnection.GetConnection();
        
        public async IAsyncEnumerable<Facility> ReadAll()
        {

            var Qref = db.Collection("facilities");
            var snap = await Qref.GetSnapshotAsync();

            foreach (var docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    var dict = docsnap.ToDictionary();
                    
                    var lat = Convert.ToDouble(dict["lat"]);
                    var longi = Convert.ToDouble(dict["long"]);
                    var name = Convert.ToString(dict["name"]);
                    
                    yield return new Facility(lat, longi, name);
                }
            }
        }

        public Facility ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}