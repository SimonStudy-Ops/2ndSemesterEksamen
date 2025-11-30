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
    }
}