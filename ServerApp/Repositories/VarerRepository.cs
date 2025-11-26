using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Repositories
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

            database = mongoClient.GetDatabase("basement");

            collection = database.GetCollection<Varer>("Varer");
        }

        public void CreateVarer(Varer vare)
        {
            collection.InsertOne(vare);
        }

        public void CreateVarer(List<Varer> varer)
        {
            collection.InsertMany(varer);
        }

        public void DeleteAll()
        {
            collection.DeleteMany(_ => true);
        }

        public void DeleteById(int varerId)
        {
            collection.DeleteOne(v => v.Varerid == varerId);
        }

        public List<Varer> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }

        public Varer GetVarerById(int varerId)
        {
            var filter = Builders<Varer>.Filter.Eq(v => v.Varerid, varerId);
            return collection.Find(filter).FirstOrDefault();
        }

        public void UpdateVarer(Varer vare)
        {
            var filter = Builders<Varer>.Filter.Eq(v => v.Varerid, vare.Varerid);
            collection.ReplaceOne(filter, vare);
        }

    }
}
