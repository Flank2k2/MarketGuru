using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using MarketGuru.Data.Entities;

namespace MarketGuruApi.Tests.Utils
{
    public class DataRepositoryClient
    {

        public async Task<Dictionary<string,object>> RetrieveApiRecommendationById(string id)
        {
            var firestoreDb =  FirestoreDb.Create(MarketGuruTestApplication.FirestoreProjectId);
            var collection = firestoreDb.Collection($"{MarketGuruTestApplication.FirestoreEnvironment}/history");

            var docRef = collection.Document(id);
            var snapshot =  await docRef.GetSnapshotAsync();
            if (snapshot.Exists == false)
                return new Dictionary<string, object>();

            return snapshot.ToDictionary();
        }
    }
}
