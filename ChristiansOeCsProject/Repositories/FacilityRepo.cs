using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    /**
     * Facility Repository er ansvarlig for at udføre CRUD aktioner med Facility entity
     */
    public class FacilityRepo : ICRUDRepo<Facility>
    {
        // Instantiere Firestore connection via FirebaseConnection static metode
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
        public async Task<Facility> Create(Facility facility)
        {
            DocumentReference documentReference = _db.Collection("facilities").Document(facility.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", facility.Latitude},
                {"long", facility.Longitude},
                {"name", facility.Name}
            };
            await documentReference.CreateAsync(data);

            return facility;
        }

        /**
         * ReadById metode tager id fra et Attraction objekt og og forsøger at hente et Firebase document snapshot med tilsvarende id.
         * Hvis dette ikke lykkedes returneres et null objekt.
         */
        public async Task<Facility> ReadById(string id)
        {
            DocumentReference docRef = _db.Collection("facilities").Document(id);
            DocumentSnapshot docsnap = await docRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                Dictionary<string, object> dict = docsnap.ToDictionary();
                double lat = Convert.ToDouble(dict["lat"]);
                double longi = Convert.ToDouble(dict["long"]);
                string name = Convert.ToString(dict["name"]);
                return new Facility(id, lat, longi, name);
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
        public async IAsyncEnumerable<Facility> ReadAll()
        {

            CollectionReference qref = _db.Collection("facilities");
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    string id = docsnap.Id;
                    yield return ReadById(id).Result;

                    /*
                    Dictionary<string, object> dict = docsnap.ToDictionary();
                    double lat = Convert.ToDouble(dict["lat"]);
                    double longi = Convert.ToDouble(dict["long"]);
                    string name = Convert.ToString(dict["name"]);
                    
                    yield return new Facility(id, lat, longi, name);
                    */
                }
            }
        }

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
        public async Task<Facility> Update(Facility facility)
        {
            DocumentReference documentReference = _db.Collection("facilities").Document(facility.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", facility.Latitude},
                {"long", facility.Longitude},
                {"name", facility.Name}
            };

            DocumentSnapshot snap = await documentReference.GetSnapshotAsync();
            if (snap.Exists)
            {
                await documentReference.SetAsync(data);
            }

            return facility;
        }

        // Delete metoden tager id på et objekt og forsøger at slette det fra databasen
        public void Delete(string id)
        {
            DocumentReference documentReference = _db.Collection("facilities").Document(id);
            documentReference.DeleteAsync();
        }
    }
}