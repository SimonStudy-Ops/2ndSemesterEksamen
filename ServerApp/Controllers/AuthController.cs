using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;
using Core.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BrugerRepository _repo;

        public AuthController(BrugerRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            // Hent ALLE brugere fra MongoDB
            var brugere = _repo.GetAll();

            // Find match på username eller mail
            var bruger = brugere.FirstOrDefault(b =>
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
}