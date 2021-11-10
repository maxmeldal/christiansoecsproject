using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class AttractionRepo : ICRUDRepo<Attraction>
    {
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();

        public void Create(Attraction t)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<Attraction> ReadAll()
        {
            var qref = _db.Collection("attractions");
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
                    
                    yield return new Attraction(id, lat, longi, name);
                }
            }
        }

        public async Task<Attraction> ReadById(string id)
        {
            var DocRef = _db.Collection("attractions").Document(id);
            var docsnap = await DocRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                var dict = docsnap.ToDictionary();
                var lat = Convert.ToDouble(dict["lat"]);
                var longi = Convert.ToDouble(dict["long"]);
                var name = Convert.ToString(dict["name"]);
                return new Attraction(id, lat, longi, name);
            }

            return null;
        }

        public Task<Attraction> Update(Attraction t)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}