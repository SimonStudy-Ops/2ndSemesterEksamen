using Microsoft.AspNetCore. Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VarerBeholdningController : ControllerBase
    {
        private readonly VarerBeholdningRepository _repo;

        public VarerBeholdningController(VarerBeholdningRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<VarerBeholdning> Get()
        {
            return _repo.GetAll();
        }

        [HttpPost]
        public void CreateVarerBeholdning([FromBody] VarerBeholdning vb)
        {
            _repo.CreateVarerBeholdning(vb);
        }

        [HttpDelete("delete")]
        public void DeleteAll()
        {
            _repo.DeleteAll();
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] VarerBeholdning vb)
        {
            // Simpel validering: id i URL og objekt skal stemme overens
            if (id != vb.VarerbeholdId)
            {
                return BadRequest("Id i URL og objekt stemmer ikke overens.");
            }

            _repo.UpdateVarerBeholdning(vb);
            return NoContent(); // 204 – OK uden body
        }
    }
}