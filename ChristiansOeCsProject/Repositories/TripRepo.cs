using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Google.Cloud.Firestore;
using Route = Microsoft.AspNetCore.Routing.Route;

namespace ChristiansOeCsProject.Repositories
{
    public class TripRepo : ICRUDRepo<Trip>
    {
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();
        private readonly AttractionService _attractionService = new AttractionService();

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
            
            CollectionReference collectionReference = documentReference.Collection("attractions");
            foreach (var tripAttraction in trip.Attractions)
            {
                DocumentReference docRef = collectionReference.Document(tripAttraction.Id);
                await docRef.CreateAsync(new Dictionary<string, object>());
            }
            
            return trip; 
        }

        public async IAsyncEnumerable<Trip> ReadAll()
        {
            var qref = _db.Collection("routes");
            var snap = await qref.GetSnapshotAsync();

            foreach (var docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    var id = docsnap.Id;
                    
                    var dict = docsnap.ToDictionary();
                    var name = Convert.ToString(dict["name"]);
                    var info = Convert.ToString(dict["info"]);
                    Theme theme;
                    if (dict["theme"].Equals("Nature"))
                    {
                        theme = Theme.Nature;
                    } 
                    else if (dict["theme"].Equals("War"))
                    {
                        theme = Theme.War;
                    }
                    else
                    {
                        theme = Theme.History;
                    }

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

        public async Task<Trip> ReadById(string id)
        {
            var DocRef = _db.Collection("routes").Document(id);
            var docsnap = await DocRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                var dict = docsnap.ToDictionary();
                var name = Convert.ToString(dict["name"]);
                var info = Convert.ToString(dict["info"]);
                Theme theme;
                if (dict["theme"].Equals("Nature"))
                {
                    theme = Theme.Nature;
                } 
                else if (dict["theme"].Equals("War"))
                {
                    theme = Theme.War;
                }
                else
                {
                    theme = Theme.History;
                }
                
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

                return new Trip(id, name, info, theme, attractions);
            }

            return null;
        }

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
            foreach (var documentSnapshot in collSnap.Documents)
            {
                await documentSnapshot.Reference.DeleteAsync();
            }
            
            //creates new attractions for the trip
            foreach (var tripAttraction in trip.Attractions)
            {
                DocumentReference docRef = collectionReference.Document(tripAttraction.Id);
                await docRef.CreateAsync(new Dictionary<string, object>());
            }

            return trip;
        }

        public async void Delete(string id)
        {
            DocumentReference documentReference = _db.Collection("routes").Document(id);
            CollectionReference collectionReference = documentReference.Collection("attractions");
            QuerySnapshot snap = await collectionReference.GetSnapshotAsync();

            foreach (var documentSnapshot in snap.Documents)
            {
                await documentSnapshot.Reference.DeleteAsync();
            }

            await documentReference.DeleteAsync();
        }
    }
}