using Microsoft.AspNetCore.Mvc;
using Core.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/kategorier")]
    public class KategoriController : ControllerBase
    {
        private readonly KategoriRepository _repo;

        public KategoriController(KategoriRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<Kategorier> Get()
        {
            return _repo.GetAll();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Kategorier kategori)
        {
            if (string.IsNullOrWhiteSpace(kategori.kategoriNavn))
            {
                return BadRequest("Kategorinavn må ikke være tomt");
            }
            
            _repo.CreateKategori(kategori);
            return Ok();
        }

        [HttpDelete("{navn}")]
        public IActionResult Delete(string navn)
        {
            _repo.DeleteByName(navn);
            return NoContent();
        }
    }
}