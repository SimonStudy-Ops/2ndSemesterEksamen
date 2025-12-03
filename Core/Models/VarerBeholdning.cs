using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models;

public class VarerBeholdning
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int VarerbeholdId {get; set;}
    
    public int Mængde {get; set;}
    public Lokalitet Lokalitet { get; set;} // Embedded
    public int VarerId {get; set;} // Reference, ikke embedded
    public string VarerNavn { get; set;} // Denormaliseret for nemmere læsning af liste da Navn i Varer sjældent vil ændre sig efter det er oprettet
    
    [BsonIgnore] // Dette felt gemmes ikke i MongoDB
    public string?  KategoriNavn { get; set; } // Denormaliseret for UI
}