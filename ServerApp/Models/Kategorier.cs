using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class Kategorier
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
   public string kategoriNavn { get; set; }
}
