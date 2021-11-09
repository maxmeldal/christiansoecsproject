using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class RestaurantRepo : ICRUDRepo<Restaurant>
    {
        private FirestoreDb db = FirebaseConnection.GetConnection();
        
        public async IAsyncEnumerable<Restaurant> ReadAll()
        {
            var qref = db.Collection("restaurants");
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
                    var url = Convert.ToString(dict["url"]);
                    if (url is "null")
                    {
                        url = null;
                    }
                    var open = Convert.ToDouble(dict["open"]);
                    var close = Convert.ToDouble(dict["close"]);
                    
                    yield return new Restaurant(id, lat, longi, name, url, open, close);
                }
            }
        }

        public async Task<Restaurant> ReadById(string id)
        {
            var DocRef = db.Collection("restaurants").Document(id);
            var docsnap = await DocRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                var dict = docsnap.ToDictionary();
                var lat = Convert.ToDouble(dict["lat"]);
                var longi = Convert.ToDouble(dict["long"]);
                var name = Convert.ToString(dict["name"]);
                var url = Convert.ToString(dict["url"]);
                if (url is "null")
                {
                    url = null;
                }
                var open = Convert.ToDouble(dict["open"]);
                var close = Convert.ToDouble(dict["close"]);
                return new Restaurant(id, lat, longi, name, url, open, close);
            }

            return null;
        }
    }
}