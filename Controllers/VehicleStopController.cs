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
    public class VehicleStopController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public VehicleStopController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/VehicleStop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleStop>>> GetVehicleStop()
        {
            var stops = await _context.VehicleStop.ToListAsync();
            
            foreach(var s in stops)
            {
                var pv = _context.PersonVehicle.SingleOrDefault(pv => pv.VehicleId == s.VehicleId);
                if(pv != null)
                {
                    var person = _context.Person.SingleOrDefault(p => p.PersonId == pv.PersonId);
                    if(person != null)
                    {
                        s.TargetNumber = person.TargetNumber;
                    }
                }
            }

            return stops;
        }

        // GET: api/VehicleStop/5
        [HttpGet("{id}")]
        public ActionResult<List<VehicleStop>> GetVehicleStop(long id)
        {
            var stops = _context.VehicleStop.Where(vs => vs.VehicleId == id);

            foreach(var s in stops)
            {
                var pv = _context.PersonVehicle.SingleOrDefault(pv => pv.VehicleId == id);
                if(pv != null)
                {
                    var person = _context.Person.SingleOrDefault(p => p.PersonId == pv.PersonId);
                    if(person != null)
                    {
                        s.TargetNumber = person.TargetNumber;
                    }
                }
            }

            if (!stops.Any())
            {
                return NotFound();
            }

            return Ok(stops);
        }

        // PUT: api/VehicleStop/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleStop(long? id, VehicleStop vehicleStop)
        {
            if (id != vehicleStop.VehicleStopId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleStop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleStopExists(id))
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

        // POST: api/VehicleStop
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VehicleStop>> PostVehicleStop(VehicleStop vehicleStop)
        {
            _context.VehicleStop.Add(vehicleStop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleStop", new { id = vehicleStop.VehicleStopId }, vehicleStop);
        }

        // DELETE: api/VehicleStop/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleStop>> DeleteVehicleStop(long? id)
        {
            var vehicleStop = await _context.VehicleStop.FindAsync(id);
            if (vehicleStop == null)
            {
                return NotFound();
            }

            _context.VehicleStop.Remove(vehicleStop);
            await _context.SaveChangesAsync();

            return vehicleStop;
        }

        private bool VehicleStopExists(long? id)
        {
            return _context.VehicleStop.Any(e => e.VehicleStopId == id);
        }
    }
}
