namespace BasementClient.Models
{
    public class VarerBeholdning
    {
        public int VarerbeholdId { get; set; }
        public int Mængde { get; set; }
        public Lokalitet? Lokalitet { get; set; }
        public int VarerId { get; set; }
        public string? VarerNavn { get; set; }
    }

    public class Lokalitet
    {
        public int LokationId { get; set; }
        public string LokationNavn { get; set; }
    }
}