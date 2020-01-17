using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsiApi;

namespace CsiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public PersonController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long? id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }


            if(person != null)
            {

                //get phones
                var phones =  _context.PersonPhone.Where(pp => pp.PersonId == person.PersonId);
                var phoneCollection = new List<Phone>();
                foreach(var phone in phones)
                    phoneCollection.Add(_context.Phone.SingleOrDefault(p => p.PhoneId == phone.PhoneId)); 
                    
                person.Phones = phoneCollection;

                //get cars 

                var vehicles = _context.PersonVehicle.Where(pv => pv.PersonId == person.PersonId);
                var vehicleCollection = new List<Vehicle>();
                foreach(var vehicle in vehicles)
                    vehicleCollection.Add(_context.Vehicle.SingleOrDefault(v => v.VehicleId == vehicle.VehicleId)); 

                person.Vehicles = vehicleCollection;
            }

            return person;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long? id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(long? id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(long? id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}
