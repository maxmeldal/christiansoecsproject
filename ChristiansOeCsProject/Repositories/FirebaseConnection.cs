using System;
using Firebase.Storage;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    /**
     * FirebaseConnection klassen benytter Christiansoe-Firebase.json filen til at oprette forbindelse til Firebase
     * Klassen har to static attributter som sikrer at hvis der en gang før er oprettet forbindelse, så bliver der ikke gjort det igen,
     * men der bliver bare brugt den samme forbindelse som før
     *
     * De to static metoder er ansvarlig for at returnere hhv. FirestoreDb object og FirebaseStorage object
     */
    public class FirebaseConnection
    {
        private static FirestoreDb _db;
        private static FirebaseStorage _storage;
        public static FirestoreDb GetConnection()
        {
            if (_db != null)
            {
                return _db;
            }
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Christiansoe-Firebase.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            _db = FirestoreDb.Create("christiansoe-4889c");
            return _db;
        }
        
        public static FirebaseStorage GetStorage (){
            if (_storage != null)
            {
                return _storage;
            }
            
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Christiansoe-Firebase.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            _storage = new FirebaseStorage("christiansoe-4889c.appspot.com");
            return _storage;
        }
    }
    
    
}