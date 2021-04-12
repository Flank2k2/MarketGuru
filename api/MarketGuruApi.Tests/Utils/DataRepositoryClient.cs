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

        public async Task<IEnumerable<DocumentSnapshot>> RetrieveApiRecommendationById(string id)
        {
            var firestoreDb =  FirestoreDb.Create(MarketGuruTestApplication.FirestoreProjectId);

            var collection = firestoreDb.Collection($"{MarketGuruTestApplication.FirestoreEnvironment}/history");
            var snapshot = await collection.WhereEqualTo("Id", id).GetSnapshotAsync();
            return snapshot.Documents;
        }
    }
}
