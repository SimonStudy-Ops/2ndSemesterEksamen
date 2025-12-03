using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models;

public class VarerBeholdning
{
    public int VarerbeholdId { get; set; }
    public int Mængde { get; set; }
    public Lokalitet? Lokalitet { get; set; }
    public int VarerId { get; set; }
    public string? VarerNavn { get; set; }
}