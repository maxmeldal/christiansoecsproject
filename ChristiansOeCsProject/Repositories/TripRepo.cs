using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    /**
     * Trip Repository er ansvarlig for at udføre CRUD aktioner med Trip entity
     */
    public class TripRepo : ICRUDRepo<Trip>
    {
        // Instantiere Firestore connection via FirebaseConnection static metode
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();
        
        // Instantiere AttractionService så der kan hentes attractions tilhørende trips
        private readonly AttractionService _attractionService = new AttractionService();

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
        public async Task<Trip> Create(Trip trip)
        {
            DocumentReference documentReference = _db.Collection("routes").Document(trip.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"name", trip.Name},
                {"info", trip.Info},
                {"theme", trip.Theme}
            };
            await documentReference.CreateAsync(data);
            
            
            // tager alle attractions hos et trip og opretter i databasen
            CollectionReference collectionReference = documentReference.Collection("attractions");
            foreach (Attraction tripAttraction in trip.Attractions)
            {
                DocumentReference docRef = collectionReference.Document(tripAttraction.Id);
                await docRef.CreateAsync(new Dictionary<string, object>());
            }
            
            return trip; 
        }

        /**
         * ReadById metode tager id fra et Attraction objekt og og forsøger at hente et Firebase document snapshot med tilsvarende id.
         * Hvis dette ikke lykkedes returneres et null objekt.
         *
         * Henter derudover også alle tilhørende attractions
         */
        public async Task<Trip> ReadById(string id)
        {
            DocumentReference docRef = _db.Collection("routes").Document(id);
            DocumentSnapshot docsnap = await docRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                Dictionary<string, object> dict = docsnap.ToDictionary();
                string name = Convert.ToString(dict["name"]);
                string info = Convert.ToString(dict["info"]);
                int themeId = Convert.ToInt32(dict["theme"]);
                
                Theme theme = themeId switch
                {
                    1 => Theme.Nature,
                    2 => Theme.History,
                    3 => Theme.War,
                    _ => Theme.None
                };

                List<Attraction> attractions = new List<Attraction>();
                CollectionReference attractionsRef = _db.Collection("routes").Document(id).Collection("attractions");
                QuerySnapshot attractionsSnap = await attractionsRef.GetSnapshotAsync();
                foreach (DocumentSnapshot attrsnap in attractionsSnap)
                {
                    if (attrsnap.Exists)
                    {
                        attractions.Add(_attractionService.ReadById(attrsnap.Id));
                    }
                        
                }

                return new Trip(id, name, info, theme, attractions);
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
        public async IAsyncEnumerable<Trip> ReadAll()
        {
            CollectionReference qref = _db.Collection("routes");
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    string id = docsnap.Id;
                    
                    var dict = docsnap.ToDictionary();
                    var name = Convert.ToString(dict["name"]);
                    var info = Convert.ToString(dict["info"]);
                    var themeId = Convert.ToInt32(dict["theme"]);
                    
                    Theme theme = themeId switch
                    {
                        1 => Theme.Nature,
                        2 => Theme.History,
                        3 => Theme.War,
                        _ => Theme.None
                    };

                    var attractions = new List<Attraction>();
                    var attractionsRef = _db.Collection("routes").Document(id).Collection("attractions");
                    var attractionsSnap = await attractionsRef.GetSnapshotAsync();
                    foreach (var attrsnap in attractionsSnap)
                    {
                        if (attrsnap.Exists)
                        {
                            attractions.Add(_attractionService.ReadById(attrsnap.Id));
                        }
                        
                    }

                    yield return new Trip(id, name, info, theme, attractions);
                }
            }
        }

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
        public async Task<Trip> Update(Trip trip)
        {
            DocumentReference documentReference = _db.Collection("routes").Document(trip.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"name", trip.Name},
                {"info", trip.Info},
                {"theme", trip.Theme}
            };

            DocumentSnapshot snap = await documentReference.GetSnapshotAsync();
            if (snap.Exists)
            {
                await documentReference.SetAsync(data);
            }

            //deletes all attractions in trip
            CollectionReference collectionReference = documentReference.Collection("attractions");
            QuerySnapshot collSnap = await collectionReference.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in collSnap.Documents)
            {
                await documentSnapshot.Reference.DeleteAsync();
            }
            
            //creates new attractions for the trip
            foreach (Attraction tripAttraction in trip.Attractions)
            {
                DocumentReference docRef = collectionReference.Document(tripAttraction.Id);
                await docRef.CreateAsync(new Dictionary<string, object>());
            }

            return trip;
        }

        /**
         * Delete metoden tager id på et objekt og forsøger at slette det fra databasen
         * Sletter først alle attractions i et trip, derefter selve trippet
         */
        public async void Delete(string id)
        {
            DocumentReference documentReference = _db.Collection("routes").Document(id);
            CollectionReference collectionReference = documentReference.Collection("attractions");
            QuerySnapshot snap = await collectionReference.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in snap.Documents)
            {
                await documentSnapshot.Reference.DeleteAsync();
            }

            await documentReference.DeleteAsync();
        }
    }
}