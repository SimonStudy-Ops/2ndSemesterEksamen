using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class VarerBeholdning
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]

    public int VarerBeholdningId { get; set; }
    public int Mængde { get; set; }
    
    public Lokalitet Lokation { get; set; }    
    public List<string> Kategorier { get; set; } = new();
}