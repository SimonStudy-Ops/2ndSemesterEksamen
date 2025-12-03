using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models;

public class Bruger
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Brugerid { get; set; }
    public string Navn { get; set; }
    public string tlfnr { get; set; }
    public string Mail {get; set; }
    public bool IsAdmin  { get; set; }
    public DateOnly opretelse { get; set; }
}