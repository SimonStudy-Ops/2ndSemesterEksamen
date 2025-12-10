using MongoDB.Driver;
using Core.Models;

namespace WebApplication1.Repositories
{
    public class BrugerRepository
    {
        private string connectionString = "mongodb+srv://eaa24mofh_db_user:mohamed123@2ndsemestereksamen.ghi4mwz.mongodb.net/?appName=2ndsemestereksamen";

        IMongoClient mongoClient;
        IMongoDatabase database;
        IMongoCollection<Bruger> collection;

        public BrugerRepository()
        {
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("basement");
            collection = database.GetCollection<Bruger>("Brugers");
        }

        public void CreateBruger(Bruger bruger)
        {
          
            collection.InsertOne(bruger);
        }

        public void CreateBruger(List<Bruger> brugere)
        {
            collection.InsertMany(brugere);
        }

        public void DeleteAll()
        {
            collection.DeleteMany(_ => true);
        }

        public void DeleteById(int brugerId)
        {
            collection.DeleteOne(b => b.Brugerid == brugerId);
        }

        public List<Bruger> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }

        public Bruger GetBrugerById(int brugerId)
        {
            var filter = Builders<Bruger>.Filter.Eq(b => b.Brugerid, brugerId);
            return collection.Find(filter).FirstOrDefault();
        }

        public void UpdateBruger(Bruger bruger)
        {
            var filter = Builders<Bruger>.Filter.Eq(b => b.Brugerid, bruger.Brugerid);
            collection.ReplaceOne(filter, bruger);
        }
    }
}