using System;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class FirebaseConnection
    {
        private static FirestoreDb _db;
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
    }
}