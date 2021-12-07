using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
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
        private readonly WebClient _webclient = new System.Net.WebClient();


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
                    
                    string video = null;
                    try
                    {
                        var videoRef = _storage.Child(id).Child("video.mp4");
                        var videoUrl = await videoRef.GetDownloadUrlAsync();
                        var videoBytes = _webclient.DownloadData(videoUrl);
                        video = Convert.ToBase64String(videoBytes);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }

                    string audio = null;
                    try
                    {
                        var audioRef = _storage.Child(id).Child("audio.mp3");
                        var audioUrl = await audioRef.GetDownloadUrlAsync();
                        var audioBytes = _webclient.DownloadData(audioUrl);
                        audio = Convert.ToBase64String(audioBytes);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }

                    yield return new Attraction(id, lat, longi, name, video, audio);
                }
            }
        }

        public async Task<Attraction> ReadById(string id)
        {
            var docRef = _db.Collection("attractions").Document(id);
            var docsnap = await docRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                var dict = docsnap.ToDictionary();
                var lat = Convert.ToDouble(dict["lat"]);
                var longi = Convert.ToDouble(dict["long"]);
                var name = Convert.ToString(dict["name"]);
                

                string video = null;
                try
                {
                    var videoRef = _storage.Child(id).Child("video.mp4");
                    var videoUrl = await videoRef.GetDownloadUrlAsync();
                    var videoBytes = _webclient.DownloadData(videoUrl);
                    video = Convert.ToBase64String(videoBytes);
                }
                catch (Exception e)
                {
                    // ignored
                }

                string audio = null;
                try
                {
                    var audioRef = _storage.Child(id).Child("audio.mp3");
                    var audioUrl = await audioRef.GetDownloadUrlAsync();
                    var audioBytes = _webclient.DownloadData(audioUrl);
                    audio = Convert.ToBase64String(audioBytes);
                }
                catch (Exception e)
                {
                    // ignored
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

        private void SetVideo(string id, string video)
        {
            var videoArr = Encoding.UTF8.GetBytes(video);
            
            var stream = new MemoryStream();
            stream.Write(videoArr, 0, videoArr.Length);
            stream.Seek(0, SeekOrigin.Begin);

            _storage.Child(id).Child("video").PutAsync(stream);
        }

        private void SetAudio(string id, string audio)
        {
            var audioArr = Encoding.UTF8.GetBytes(audio);
            
            var stream = new MemoryStream();
            stream.Write(audioArr, 0, audioArr.Length);
            stream.Seek(0, SeekOrigin.Begin);

            _storage.Child(id).Child("audio").PutAsync(stream);
        }

        public void Delete(string id)
        {
            DocumentReference documentReference = _db.Collection("attractions").Document(id);
            documentReference.DeleteAsync();
            try
            {
                _storage.Child(id).Child("video").DeleteAsync();

            }
            catch (Exception e)
            {
                // ignored
            }
            try
            {
                _storage.Child(id).Child("audio").DeleteAsync();
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}