using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Firebase.Storage;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class AttractionRepo : ICRUDRepo<Attraction>
    {
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();
        private readonly FirebaseStorage _storage = FirebaseConnection.GetStorage();

        public async Task<Attraction> Create(Attraction attraction)
        {
            DocumentReference documentReference = _db.Collection("attractions").Document(attraction.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"lat", attraction.Latitude},
                {"long", attraction.Longitude},
                {"name", attraction.Name},
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
                    
                    // byte[] video;
                    // if (dict["video"] != null)
                    // {
                    //     var videoBlob = (Blob) dict["video"];
                    //     video = videoBlob.ByteString.ToByteArray();
                    // }
                    // else
                    // {
                    //     video = null;
                    // }
                    //
                    // byte[] audio;
                    // if (dict["audio"] != null)
                    // {
                    //     var audioBlob = (Blob) dict["audio"];
                    //     audio = audioBlob.ByteString.ToByteArray();  
                    // }
                    // else
                    // {
                    //     audio = null;
                    // }
                    
                    yield return new Attraction(id, lat, longi, name, "video", "audio");
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

                // byte[] video;
                // if (dict["video"] != null)
                // {
                //     var videoBlob = (Blob) dict["video"];
                //     video = videoBlob.ByteString.ToByteArray();
                // }
                // else
                // {
                //     video = null;
                // }
                //
                // byte[] audio;
                // if (dict["audio"] != null)
                // {
                //     var audioBlob = (Blob) dict["audio"];
                //     audio = audioBlob.ByteString.ToByteArray();  
                // }
                // else
                // {
                //     audio = null;
                // }

                //var path = "gs://christiansoe-4889c.appspot.com/" + id;
                //var video = _storage.Child(path)

                return new Attraction(id, lat, longi, name, "video", "audio");
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

            if (attraction.Video != null)
            {
                SetVideo(attraction.Id, attraction.Video);
            }

            if (attraction.Audio != null)
            {
                SetAudio(attraction.Id, attraction.Audio);
            }

            return attraction;
        }

        public void SetVideo(String id, string video)
        {
            var videoArr = Encoding.UTF8.GetBytes(video);
            
            var stream = new MemoryStream();
            stream.Write(videoArr, 0, videoArr.Length);
            stream.Seek(0, SeekOrigin.Begin);

            _storage.Child(id).PutAsync(stream);
        }

        public void SetAudio(string id, string audio)
        {
            var audioArr = Encoding.UTF8.GetBytes(audio);
            
            var stream = new MemoryStream();
            stream.Write(audioArr, 0, audioArr.Length);
            stream.Seek(0, SeekOrigin.Begin);

            _storage.Child(id).PutAsync(stream);
        }

        public void Delete(string id)
        {
            DocumentReference documentReference = _db.Collection("attractions").Document(id);
            documentReference.DeleteAsync();
        }
    }
}