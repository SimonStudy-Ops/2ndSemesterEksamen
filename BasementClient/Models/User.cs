namespace BasementClient.Models;

public class User
{
    public string Navn { get; set; }
    public string Email { get; set; }
    public int Tlfnr { get; set; }
    public bool IsAdmin { get; set; } = false;
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime Oprettet { get; set; } = DateTime.Now;

}