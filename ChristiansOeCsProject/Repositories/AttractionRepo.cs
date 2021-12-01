using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class AttractionRepo : ICRUDRepo<Attraction>
    {
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();

        public async Task<Attraction> Create(Attraction attraction)
        {
            DocumentReference documentReference = _db.Collection("attractions").Document(attraction.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", attraction.Latitude},
                {"long", attraction.Longitude},
                {"name", attraction.Name},
                {"video", attraction.Video},
                {"audio", attraction.Audio}
            };
            await documentReference.CreateAsync(data);

            return attraction;
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
                    
                    var videoBlob = (Blob) dict["video"];
                    var video = videoBlob.ByteString.ToByteArray();
                
                    var audioBlob = (Blob) dict["audio"];
                    var audio = audioBlob.ByteString.ToByteArray();
                    
                    yield return new Attraction(id, lat, longi, name, video, audio);
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

                byte[] video;
                if (dict["video"] != null)
                {
                    var videoBlob = (Blob) dict["video"];
                    video = videoBlob.ByteString.ToByteArray();
                }
                else
                {
                    video = null;
                }

                byte[] audio;
                if (dict["audio"] != null)
                {
                    var audioBlob = (Blob) dict["audio"];
                    audio = audioBlob.ByteString.ToByteArray();  
                }
                else
                {
                    audio = null;
                }

                return new Attraction(id, lat, longi, name, video, audio);
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
                {"name", attraction.Name},
                {"video", attraction.Video},
                {"audio", attraction.Audio}
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
            DocumentReference documentReference = _db.Collection("attractions").Document(id);
            documentReference.DeleteAsync();
        }
    }
}