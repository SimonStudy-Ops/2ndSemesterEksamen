using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1. Repositories;

namespace WebApplication1. Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VarelagerController : ControllerBase
    {
        private readonly VarelagerRepository _repo;

        public VarelagerController(VarelagerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<Varelager> Get()
        {
            return _repo.GetAll();
        }

        [HttpPost]
        public void CreateVarelager([FromBody] Varelager varelager)
        {
            _repo.CreateVarelager(varelager);
        }

        [HttpDelete("delete/{id:int}")]
        public void Delete(int id)
        {
            _repo.DeleteById(id);
        }
    }
}