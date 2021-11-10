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

        public void Create(Trip trip)
        {
            DocumentReference documentReference = _db.Collection("routes").Document(trip.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"name", trip.Name},
                {"info", trip.Info},
                {"theme", trip.Theme}
            };
            documentReference.CreateAsync(data);
            
            CollectionReference collectionReference = documentReference.Collection("attractions");
            foreach (var tripAttraction in trip.Attractions)
            {
                DocumentReference docRef = collectionReference.Document(tripAttraction.Id);
                docRef.CreateAsync(new Dictionary<string, object>());
            }
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

        public Task<Trip> Update(Trip t)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}