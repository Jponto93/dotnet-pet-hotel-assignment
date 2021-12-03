using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets()
        {
            return new List<Pet>();
        }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }

        // POST 
        [HttpPost]
        public IActionResult Post(Pet pet)
        {
            pet.checkedInAt = null;
            
            _context.Add(pet);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Post), new { id = pet.id }, pet);
        }

        // DEL /api/pets/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("deleting with id: " + id);
            Pet pet = _context.Pets.SingleOrDefault(pet => pet.id == id);

            if(pet is null)
            {
                // not found
                return NotFound(); // 404
            }
            // delete that pet
            _context.Pets.Remove(pet);
            _context.SaveChanges();

            // respond
            return NoContent(); // 204

        }
        // PUT /api/pets/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Pet pet)
        {
            // Confirming we are in Pets PUT
            Console.WriteLine("In Pets Put");

            if (id != pet.id)
            {
                return BadRequest(); // 404
            }

            _context.Update(pet);
            _context.SaveChanges();
            return NoContent();
        }
        // PUT id check in /api/pets/id/checkout
        [HttpPut("{id}/checkout")]
        public IActionResult CheckoutPut(int id, Pet pet)
        {
            // Confirming we are in Pets PUT
            Console.WriteLine("In Pets PUT");

            if(id != pet.id)
            {
                return BadRequest(); // 404
            }

            pet.checkedInAt = null;

            _context.Update(pet);
            _context.SaveChanges();
            return NoContent();
        }
        // PUT id check in /api/pets/id/checkin
        [HttpPut("{id}/checkin")]
        public IActionResult CheckinPut(int id, Pet pet)
        {
            // Confirming we are in Pets PUT
            Console.WriteLine("In Pets PUT");

            if(id != pet.id)
            {
                return BadRequest(); // 404
            }

            pet.checkedInAt = DateTime.Now;

            _context.Update(pet);
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}
