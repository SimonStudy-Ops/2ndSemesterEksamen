using MongoDB.Driver;
using WebApplication1.Models;

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
    }
}