using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using Core.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMongoDatabase _db;
    public AuthController(IMongoDatabase db) => _db = db;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var brugereCol = _db.GetCollection<Bruger>("Brugers");
        var filterMail = Builders<Bruger>.Filter.Regex("Mail", new BsonRegularExpression($"^{Regex.Escape(req.Username)}$", "i"));
        var filterNavn = Builders<Bruger>.Filter.Regex("Navn", new BsonRegularExpression($"^{Regex.Escape(req.Username)}$", "i"));
        var filterPwd = Builders<Bruger>.Filter.Eq("Password", req.Password);
        var filter = Builders<Bruger>.Filter.And(Builders<Bruger>.Filter.Or(filterMail, filterNavn), filterPwd);
        var bruger = brugereCol.Find(filter).FirstOrDefault();
        if (bruger == null)
            return Unauthorized("Forkert brugernavn eller adgangskode.");
        var response = new
        {
            token = $"{bruger.Brugerid}-token",
            username = bruger.Navn,
            isAdmin = bruger.IsAdmin,
            id = bruger.Brugerid
        };
        return Ok(response);
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}