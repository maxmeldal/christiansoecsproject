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
        private FirestoreDb db = FirebaseConnection.GetConnection();
        private AttractionService service = new AttractionService();

        public async IAsyncEnumerable<Trip> ReadAll()
        {
            var qref = db.Collection("routes");
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
                    var attractionsRef = db.Collection("routes").Document(id).Collection("attractions");
                    var attractionsSnap = await attractionsRef.GetSnapshotAsync();
                    foreach (var attrsnap in attractionsSnap)
                    {
                        if (attrsnap.Exists)
                        {
                            attractions.Add(service.ReadById(attrsnap.Id));
                        }
                        
                    }

                    yield return new Trip(id, name, info, theme, attractions);
                }
            }
        }

        public async Task<Trip> ReadById(string id)
        {
            var DocRef = db.Collection("routes").Document(id);
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
                var attractionsRef = db.Collection("routes").Document(id).Collection("attractions");
                var attractionsSnap = await attractionsRef.GetSnapshotAsync();
                foreach (var attrsnap in attractionsSnap)
                {
                    if (attrsnap.Exists)
                    {
                        attractions.Add(service.ReadById(attrsnap.Id));
                    }
                        
                }

                return new Trip(id, name, info, theme, attractions);
            }

            return null;
        }
    }
}