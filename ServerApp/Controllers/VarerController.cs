using Microsoft.AspNetCore.Mvc;
using Core.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/varer")]
    public class VarerController : ControllerBase
    {
        private readonly VarerRepository _repo;

        public VarerController(VarerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<Varer> Get()
        {
            return _repo.GetAll();
        }

        [HttpPost]
        public void CreateVarer([FromBody] Varer p)
        {
            _repo.CreateVarer(p);
        }

        [HttpDelete("delete/{id:int}")]
        public void Delete(int id)
        {
            _repo.DeleteById(id);
        }

        [HttpDelete("delete")]
        public void DeleteByQuery([FromQuery] int id)
        {
            _repo.DeleteById(id);
        }
    }
}

