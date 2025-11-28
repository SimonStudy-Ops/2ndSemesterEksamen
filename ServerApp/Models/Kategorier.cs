using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class Kategorier
{
    [BsonId]
   public string kategoriNavn { get; set; }
}
