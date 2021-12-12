using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using Firebase.Storage;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    /**
     * Attraction Repository er ansvarlig for at udføre CRUD aktioner med Attraction entity
     * Ulig de andre repositories, instantiere attraction repo også en firebase storage,
     * så den kan arbejde med lyd- og videofiler
     *
     * OBS: Oprettelse og afhentelse af filer fungerer ikke. API'en kan godt sende filer til firebase storage,
     * men de er broken ved ankomst. Servicen forsøger at Base64 String og konvertere den til et byte[] som så sendes med.
     * Ved ankomst kan det ses at filen er komprimeret og den byte[] man får med tilbage når man henter er ikke magen til den man sendte
     */
    public class AttractionRepo : ICRUDRepo<Attraction>
    {
        //Instantiere firestore connection og storage connection via static methods i FirebaseConnection klassen
        private readonly FirestoreDb _db = FirebaseConnection.GetConnection();
        private readonly FirebaseStorage _storage = FirebaseConnection.GetStorage();
        
        // Webclient skal bruges til at hente filer fra Storage
        private readonly WebClient _webclient = new System.Net.WebClient();
        
        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         */
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

        /**
         * ReadById metode tager id fra et Attraction objekt og og forsøger at hente et Firebase document snapshot med tilsvarende id.
         * Hvis dette ikke lykkedes returneres et null objekt.
         * Metoden vil ligeledes forsøge at hente video og lydfiler, ved at få downloadurl fra en storage reference, og derefter bruge
         * webclient til at downloade og derefter konvertere byte[] til Base64 String
         *
         * Exceptions er ignored - mulig implementation: send mail til error-mailserver
         */
        public async Task<Attraction> ReadById(string id)
        {
            DocumentReference docRef = _db.Collection("attractions").Document(id);
            DocumentSnapshot docsnap = await docRef.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                Dictionary<string, object> dict = docsnap.ToDictionary();
                double lat = Convert.ToDouble(dict["lat"]);
                double longi = Convert.ToDouble(dict["long"]);
                string name = Convert.ToString(dict["name"]);
                

                string video = null;
                try
                {
                    FirebaseStorageReference videoRef = _storage.Child(id).Child("video");
                    string videoUrl = await videoRef.GetDownloadUrlAsync();
                    byte[] videoBytes = _webclient.DownloadData(videoUrl);
                    video = Convert.ToBase64String(videoBytes);
                }
                catch (Exception e)
                {
                    // ignored
                }

                string audio = null;
                try
                {
                    FirebaseStorageReference audioRef = _storage.Child(id).Child("audio");
                    string audioUrl = await audioRef.GetDownloadUrlAsync();
                    byte[] audioBytes = _webclient.DownloadData(audioUrl);
                    audio = Convert.ToBase64String(audioBytes);
                }
                catch (Exception e)
                {
                    // ignored
                }

                string image = null;
                try
                {
                    FirebaseStorageReference imageRef = _storage.Child(id).Child("image");
                    string imageUrl = await imageRef.GetDownloadUrlAsync();
                    byte[] imageBytes = _webclient.DownloadData(imageUrl);
                    image = Convert.ToBase64String(imageBytes);
                }
                catch (Exception e)
                {
                    // ignored
                }

                return new Attraction(id, lat, longi, name, video, audio, image);
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
        public async IAsyncEnumerable<Attraction> ReadAll()
        {
            CollectionReference qref = _db.Collection("attractions");
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
                    
                    string video = null;
                    try
                    {
                        FirebaseStorageReference videoRef = _storage.Child(id).Child("video");
                        string videoUrl = await videoRef.GetDownloadUrlAsync();
                        byte[] videoBytes = _webclient.DownloadData(videoUrl);
                        video = Convert.ToBase64String(videoBytes);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }

                    string audio = null;
                    try
                    {
                        FirebaseStorageReference audioRef = _storage.Child(id).Child("audio");
                        string audioUrl = await audioRef.GetDownloadUrlAsync();
                        byte[] audioBytes = _webclient.DownloadData(audioUrl);
                        audio = Convert.ToBase64String(audioBytes);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                    
                    string image = null;
                    try
                    {
                        FirebaseStorageReference imageRef = _storage.Child(id).Child("image");
                        string imageUrl = await imageRef.GetDownloadUrlAsync();
                        byte[] imageBytes = _webclient.DownloadData(imageUrl);
                        image = Convert.ToBase64String(imageBytes);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }

                    yield return new Attraction(id, lat, longi, name, video, audio, image);
                    
                }
            }
        }

        /**
         * For at oprette/opdatere i databasen skal alt informationen tages fra objektet og gemmes i et Dictionary<String, Object>
         * Dette dictionary sendes med via en DocumentReference taget fra Firestore
         * Metoden udføres asynkront så user operations kan fortsætte
         *
         * I tilfælde af at attraction har enten video eller audio tilknyttet så vil de også forsøges at blive oprettet
         */
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

            if (attraction.Video != null) SetFile(attraction.Id, attraction.Video, "video");
            if (attraction.Audio != null) SetFile(attraction.Id, attraction.Audio, "audio");
            if (attraction.Image != null) SetFile(attraction.Id, attraction.Image, "image");

            return attraction;
        }

        /**
         * SetFile tager først id på den attraction som filen tilhører, dernæst filen i Base64 String format,
         * og sidst navnet filen skal have i Storage
         */
        private void SetFile(string id, string file, string name)
        {
            // konverter Base64 Stringen til et byte[]
            byte[] fileArr = Encoding.UTF8.GetBytes(file);
            
            // Skriv byte[] til memorystream
            MemoryStream stream = new MemoryStream();
            stream.Write(fileArr, 0, fileArr.Length);
            stream.Seek(0, SeekOrigin.Begin);

            // send stream til storage
            _storage.Child(id).Child(name).PutAsync(stream, CancellationToken.None);
        }

        /**
         * Delete metoden tager id på et objekt og forsøger at slette det fra databasen
         * I tilfælde af attraction vil den også forsøge at slette alle tilhørende filer
         */
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

            try
            {
                _storage.Child(id).Child("image").DeleteAsync();
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}