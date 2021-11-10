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

        public void Create(Attraction attraction)
        {
            DocumentReference documentReference = _db.Collection("attractions").Document(attraction.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", attraction.Latitude},
                {"long", attraction.Longitude},
                {"name", attraction.Name}
            };
            documentReference.CreateAsync(data);
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

        public async Task<Attraction> Update(Attraction attraction)
        {
            DocumentReference documentReference = _db.Collection("attractions").Document(attraction.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", attraction.Latitude},
                {"long", attraction.Longitude},
                {"name", attraction.Name}
            };

            DocumentSnapshot snap = await documentReference.GetSnapshotAsync();
            if (snap.Exists)
            {
                await documentReference.SetAsync(data);
            }

            return attraction;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}