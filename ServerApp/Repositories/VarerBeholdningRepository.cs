using MongoDB.Driver;
using Core.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories
{
    public class VarerBeholdningRepository
    {
        private string connectionString = "mongodb://localhost:27017";

        IMongoClient mongoClient;
        IMongoDatabase database;
        IMongoCollection<VarerBeholdning> collection;

        public VarerBeholdningRepository()
        {
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("basement");
            collection = database.GetCollection<VarerBeholdning>("VarerBeholdning");
        }

        public void CreateVarerBeholdning(VarerBeholdning vb)
        {
            collection.InsertOne(vb);
        }

        public void CreateVarerBeholdning(List<VarerBeholdning> vbs)
        {
            collection.InsertMany(vbs);
        }

        public void DeleteAll()
        {
            collection.DeleteMany(_ => true);
        }

        public void DeleteById(int varerbeholdId)
        {
            var filter = Builders<VarerBeholdning>.Filter
                .Eq(x => x.VarerbeholdId, varerbeholdId);
            
            collection.DeleteOne(filter);
        }

        public List<VarerBeholdning> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }
        public void UpdateVarerBeholdning(VarerBeholdning vb)
        {
            var filter = Builders<VarerBeholdning>.Filter
                .Eq(x => x.VarerbeholdId, vb.VarerbeholdId);

            collection.ReplaceOne(filter, vb);
        }
    }
}