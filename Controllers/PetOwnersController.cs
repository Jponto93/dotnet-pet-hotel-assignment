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
        public PetOwnersController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetPets() {
            return new List<PetOwner>();
        }

        // DEL /api/petOwners/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("deleting with id: " + id);
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(petOwner => petOwner.id == id);

            if(petOwner is null){
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
