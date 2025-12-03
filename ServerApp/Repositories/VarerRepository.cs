using MongoDB.Driver;
using Core.Models;

namespace WebApplication1. Repositories
{
    public class VarerRepository
    {
        private string connectionString = "mongodb://localhost:27017";

        IMongoClient mongoClient;
        IMongoDatabase database;
        IMongoCollection<Varer> collection;

        public VarerRepository()
        {
            mongoClient = new MongoClient(connectionString);
            database = mongoClient. GetDatabase("basement");
            collection = database.GetCollection<Varer>("Varer");
        }

        public void CreateVarer(Varer vare)
        {
            // Auto-increment: Find højeste Varerid og tilføj 1
            var allVarer = collection.Find(_ => true).ToList();
            int maxId = 0;
            foreach (var existing in allVarer)
            {
                if (existing.Varerid > maxId)
                {
                    maxId = existing. Varerid;
                }
            }
            vare.Varerid = maxId + 1;
            
            collection.InsertOne(vare);
            Console.WriteLine($"Oprettet vare med Varerid: {vare.Varerid}");
        }

        public void DeleteById(int varerId)
        {
            collection.DeleteOne(v => v.Varerid == varerId);
        }

        public List<Varer> GetAll() 
        {
            return collection.Find(_ => true).ToList();
        }
    }
}