using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/login")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var bruger = DataStore.Brugere
            .FirstOrDefault(b =>
                (b.Mail.Equals(req.Username, StringComparison.OrdinalIgnoreCase)
                 || b.Navn.Equals(req.Username, StringComparison.OrdinalIgnoreCase))
                && b.Password == req.Password);

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