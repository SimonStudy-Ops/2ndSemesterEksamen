using Microsoft.AspNetCore.Mvc;
using Core.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/Bruger")]
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
        var bruger = _repo.GetAll();
        return Ok(bruger);
    }

    [HttpPost]
    public IActionResult CreateBruger([FromBody] Bruger bruger)
    {
        _repo.CreateBruger(bruger);
        return Ok(bruger);
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBruger(int id)
    {
        // Slet brugeren via repository
        _repo.DeleteById(id);
        // 204 betyder: slettet ok, ingen indhold i svar
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBruger(int id, [FromBody] Bruger bruger)
    {
        if (bruger == null)
            return BadRequest("Bruger mangler.");

        bruger.Brugerid = id; // brug altid id fra URL

        _repo.UpdateBruger(bruger);
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
}