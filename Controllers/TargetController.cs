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
    public class TargetController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public TargetController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetTarget()
        {
            return await _context.Person.Where(p => p.TargetNumber != null).ToListAsync();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetTarget(long? id)
        {
            var person = await _context.Person.FindAsync(id);

            if(!person.TargetNumber.HasValue)
                return NotFound();

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

    }
}
