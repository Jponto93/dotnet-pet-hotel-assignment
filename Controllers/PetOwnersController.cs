using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetPets()
        {
            // return new List<PetOwner>();
            return _context.PetOwners.Include(petOwner => petOwner.petList);
        }

        //GET ID 
        [HttpGet("{id}")]
        public ActionResult<PetOwner> GetById(int id)
        {
            Console.WriteLine("get by id: " + id);

            PetOwner petOwner = _context.PetOwners
            .Include(petOwner => petOwner.petList)
            .SingleOrDefault(petOwner => petOwner.id == id);

            if (petOwner == null)
            {
                return NotFound(); //404
            }
            return petOwner;

        }

        // POST /api/PetOwners
        [HttpPost]
        public IActionResult Post(PetOwner petOwner)
        {
            // Confirming that we are in POST.
            Console.WriteLine("In PetOwner POST");
            _context.Add(petOwner);
            _context.SaveChanges();
            // Return the location of the newly created pet owner.
            return CreatedAtAction(nameof(Post), new { id = petOwner.id }, petOwner);
        }

        // PUT /api/PetOwners/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, PetOwner petOwner)
        {
            // Confirming that we are in PUT.
            Console.WriteLine("In PetOwner PUT");

            // Does ID exist?
            if (id != petOwner.id)
            {
                return BadRequest(); // 404
            }

            _context.Update(petOwner);
            _context.SaveChanges();
            return NoContent();
        }

        // DEL /api/petOwners/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("deleting with id: " + id);
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(petOwner => petOwner.id == id);

            if (petOwner is null)
            {
                // not found
                return NotFound(); // 404
            }
            // delete that pet owner
            _context.PetOwners.Remove(petOwner);
            _context.SaveChanges();

            // respond
            return NoContent(); // 204
        }
    }
}
