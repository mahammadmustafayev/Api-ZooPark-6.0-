using Microsoft.AspNetCore.Mvc;
using Zoopark.DAL;
using Zoopark.DTOs;
using Zoopark.Models;

namespace Zoopark.Controllers
{
    [Route("api/[controller]/[action]")]
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
            return Ok(_context.Animals.OrderByDescending(c => c.Id));
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            Animal animal = _context.Animals.FirstOrDefault(a => a.Id == id);
            if (animal is null) return StatusCode(StatusCodes.Status404NotFound, new { statuscode = 404, message = "Ay qoyun bele sey yoxdur" });
            return Ok(animal);
        }
        [HttpPost]
        public IActionResult Create(CreateAnimalDto createAnimal)
        {
            Animal animal = new Animal
            {
                Name = createAnimal.Name,
                Gender = createAnimal.Gender,
                Color = createAnimal.Color,
                ImageUrl = createAnimal.ImageUrl,
                AcceptedTime = DateTime.Now
            };
            _context.Animals.Add(animal);
            _context.SaveChanges();
            return NoContent();
        }

        //[HttpGet("{id}")]
        //public IActionResult Update(int id)
        //{
        //    Animal animaldb = _context.Animals.FirstOrDefault(a => a.Id == id);
        //    if (animaldb is null) return StatusCode(StatusCodes.Status404NotFound, new { statuscode = 404, message = "Ay qoyun filan sey" });

        //    return Ok(animaldb);
        //}
        [HttpPut]
        public IActionResult Update(Animal animal)
        {
            Animal existanimal = _context.Animals.FirstOrDefault(a => a.Id == animal.Id);
            if (existanimal is null) return StatusCode(StatusCodes.Status404NotFound, new { statuscode = 404, message = "Ay qoyun filan sey" });
            existanimal.Name = animal.Name;
            existanimal.AcceptedTime = animal.AcceptedTime;
            existanimal.Gender = animal.Gender;
            existanimal.Color = animal.Color;
            existanimal.ImageUrl = animal.ImageUrl;
            _context.SaveChanges();
            return NoContent();
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
