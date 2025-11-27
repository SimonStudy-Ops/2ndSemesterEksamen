<<<<<<< Updated upstream
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrugerController : ControllerBase
{
    private readonly BrugerRepository _repo;

    public BrugerController(BrugerRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<List<Bruger>> GetAll()
    {
        var brugere = _repo.GetAll();
        return Ok(brugere);
    }

    [HttpPost]
    public IActionResult CreateBruger([FromBody] Bruger bruger)
    {
        _repo.CreateBruger(bruger);
        return Ok(bruger);
    }

    [HttpPost("seed-admin")]
    public IActionResult SeedAdmin()
    {
        _repo.DeleteAll();

        var admin = new Bruger
        {
            Brugerid = 1,
            Navn = "Admin",
            tlfnr = "12345678",
            Mail = "admin@test.dk",
            IsAdmin = true,
            opretelse = new DateOnly(2024, 1, 1)
        };

        _repo.CreateBruger(admin);

        return Ok(admin);
    }
=======
namespace WebApplication1.Controllers;

public class BrugerController
{
    
>>>>>>> Stashed changes
}