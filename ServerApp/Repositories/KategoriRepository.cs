using MongoDB.Driver;
using Core.Models;

namespace WebApplication1.Repositories
{
    public class KategoriRepository
    {
        private string connectionString = "mongodb+srv://eaa24mofh_db_user:mohamed123@2ndsemestereksamen.ghi4mwz.mongodb.net/?appName=2ndsemestereksamen";

        IMongoClient mongoClient;
        IMongoDatabase database;
        IMongoCollection<Kategorier> collection;

        public KategoriRepository()
        {
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("basement");
            collection = database.GetCollection<Kategorier>("Kategorier");
        }

        public void CreateKategori(Kategorier kategori)
        {
            collection.InsertOne(kategori);
        }

        public List<Kategorier> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }
    }
}