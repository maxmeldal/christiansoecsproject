using System;
using Google.Cloud.Firestore;

namespace ChristiansOeCsProject.Repositories
{
    public class FirebaseConnection
    {
        public static FirestoreDb GetConnection()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Christiansoe-Firebase.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            FirestoreDb db = FirestoreDb.Create("christiansoe-4889c");
            return db;
        }
    }
}