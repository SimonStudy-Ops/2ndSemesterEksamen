using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models;

public class Varer
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Varerid { get; set; }  // Dette er både _id OG Varerid
    
    public string Navn { get; set; }
    public string Enhed { get; set; }
    public string Beskrivelse { get; set; }
    public string Billede { get; set; }
    public Kategorier Kategorier { get; set; } // Embedded
    public int MinimumsBeholdning { get; set; } = 0;
}