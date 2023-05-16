using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zoopark.DAL;
using Zoopark.DTOs;
using Zoopark.Models;

namespace Zoopark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooPark : ControllerBase
    {
        private readonly AppDbContext _context;

        public ZooPark(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult All()
        {
            return Ok(_context.Animals);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Animal animal = _context.Animals.FirstOrDefault(a => a.Id == id); 
            if (animal is null) return StatusCode(StatusCodes.Status404NotFound, new {statuscode=404,message="Ay qoyun bele sey yoxdur"});
            return Ok(animal);
        }
        [HttpPost]
        public IActionResult Create(CreateAnimalDto createAnimal)
        {
            Animal animal = new Animal
            {
                Name= createAnimal.Name,
                Gender=createAnimal.Gender,
                Color=createAnimal.Color,
                ImageUrl=createAnimal.ImageUrl,
                AcceptedTime=DateTime.Now
            };
            _context.Animals.Add(animal);   
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateAnimalDto animal)
        {
            Animal animaldb=_context.Animals.FirstOrDefault(a=>a.Id==id);
            if (animaldb is null) return StatusCode(StatusCodes.Status404NotFound, new { statuscode = 404, message = "Ay qoyun filan sey" });
            animaldb.Name = animal.Name;
            
            animaldb.Color=animal.Color;
            animaldb.ImageUrl = animal.ImageUrl;
            _context.SaveChanges();
            return Ok(animal);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Animal animal = _context.Animals.Find(id);
            if (animal is null) return StatusCode(StatusCodes.Status404NotFound, new { statuscode = 404, message = "Ay qoyun filan sey" });
            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
