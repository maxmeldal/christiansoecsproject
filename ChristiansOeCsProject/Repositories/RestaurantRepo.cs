using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    /**
     * Restaurant Repository er ansvarlig for at udføre CRUD aktioner med Restaurant entity
     */
    public class RestaurantRepo : ICRUDRepo<Restaurant>
    {
        // Instantiere Firestore connection via FirebaseConnection static metode
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
        public async Task<Restaurant> Create(Restaurant restaurant)
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
            await documentReference.CreateAsync(data);

            return restaurant;
        }

        /**
         * ReadById metode tager id fra et Attraction objekt og og forsøger at hente et Firebase document snapshot med tilsvarende id.
         * Hvis dette ikke lykkedes returneres et null objekt.
         */
        public async Task<Restaurant> ReadById(string id)
        {
            var DocRef = _db.Collection("restaurants").Document(id);
            var docsnap = await DocRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                Dictionary<string, object> dict = docsnap.ToDictionary();
                double lat = Convert.ToDouble(dict["lat"]);
                double longi = Convert.ToDouble(dict["long"]);
                string name = Convert.ToString(dict["name"]);
                string url = Convert.ToString(dict["url"]);
                if (url is "null")
                {
                    url = null;
                }
                double open = Convert.ToDouble(dict["open"]);
                double close = Convert.ToDouble(dict["close"]);
                return new Restaurant(id, lat, longi, name, url, open, close);
            }

            return null;
        }

        /**
         * ReadAll metode er et loop af ReadById metoden (se ovenstående), men istedet for at tage id, så tager den bare alle
         * document referencer fra en collection og gemmer i en CollectionReference
         *
         * Metoden returnerer en Async Enumerable som betyder at samlingen vil bliver returneret efter hver gang et nyt objekt er blevet læst
         * Dette betyder at user operations kan fortsætte mens, applikationen arbejder
         */
        public async IAsyncEnumerable<Restaurant> ReadAll()
        {
            CollectionReference qref = _db.Collection("restaurants");
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    string id = docsnap.Id;
                    
                    Dictionary<string, object> dict = docsnap.ToDictionary();
                    double lat = Convert.ToDouble(dict["lat"]);
                    double longi = Convert.ToDouble(dict["long"]);
                    string name = Convert.ToString(dict["name"]);
                    string url = Convert.ToString(dict["url"]);
                    if (url is "null")
                    {
                        url = null;
                    }
                    double open = Convert.ToDouble(dict["open"]);
                    double close = Convert.ToDouble(dict["close"]);
                    
                    yield return new Restaurant(id, lat, longi, name, url, open, close);
                }
            }
        }

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
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

        // Delete metoden tager id på et objekt og forsøger at slette det fra databasen
        public void Delete(string id)
        {
            DocumentReference documentReference = _db.Collection("restaurants").Document(id);
            documentReference.DeleteAsync();
        }
    }
}