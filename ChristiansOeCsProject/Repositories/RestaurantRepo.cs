using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class RestaurantRepo : ICRUDRepo<Restaurant>
    {
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();

        public void Create(Restaurant restaurant)
        {
            DocumentReference documentReference = _db.Collection("restaurants").Document(restaurant.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", restaurant.Latitude},
                {"long", restaurant.Longitude},
                {"name", restaurant.Name},
                {"url", restaurant.Url},
                {"open", restaurant.Open},
                {"close", restaurant.Close}
            };
            documentReference.CreateAsync(data);
        }

        public async IAsyncEnumerable<Restaurant> ReadAll()
        {
            var qref = _db.Collection("restaurants");
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
            var DocRef = _db.Collection("restaurants").Document(id);
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

        public async Task<Restaurant> Update(Restaurant restaurant)
        {
            DocumentReference documentReference = _db.Collection("restaurants").Document(restaurant.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", restaurant.Latitude},
                {"long", restaurant.Longitude},
                {"name", restaurant.Name},
                {"url", restaurant.Url},
                {"open", restaurant.Open},
                {"close", restaurant.Close}
            };

            DocumentSnapshot snap = await documentReference.GetSnapshotAsync();
            if (snap.Exists)
            {
                await documentReference.SetAsync(data);
            }

            return restaurant;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}