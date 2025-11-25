using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class Kategorier
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public string Øl { get; set; }
    public string Fadøl { get; set; }
    public string Spiritus { get; set; }
    public string Sodavand {get; set; }
    public string Energidrik { get; set; }
    public string Juice { get; set; }
    public string Konfekture { get; set; } 
}
