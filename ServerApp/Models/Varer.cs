using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models;

public class Varer
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Varerid { get; set; }
    public string Navn  { get; set; }
    public int Mængde { get; set; }
    public string Enhed {get; set; }
    public DateOnly Udløbsdato { get; set; }
    public string Beskrivelse { get; set; }
    public string Billede { get; set; }
    
    
    public Lokalitet Lokation { get; set; }    
    public List<string> Kategorier { get; set; } = new();
}




