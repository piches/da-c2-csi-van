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
    public class VehicleSurveillanceController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public VehicleSurveillanceController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/VehicleSurveillance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveillanceVehicle>>> GetSurveillanceVehicle()
        {
            return await _context.SurveillanceVehicle.ToListAsync();
        }

        // GET: api/VehicleSurveillance/5
        [HttpGet("{id}")]
        public ActionResult<List<SurveillanceVehicle>> GetSurveillanceVehicle(long id)
        {
             var surveillanceVehicle =  _context.SurveillanceVehicle.Where(sv => sv.VehicleId == id);

            if (surveillanceVehicle == null)
            {
                return NotFound();
            }

            foreach(var result in surveillanceVehicle)
            {
                result.Observation = _context.SurveillanceObservation.SingleOrDefault(o => o.ObservationId == result.ObservationId);
            }

            return Ok(surveillanceVehicle);
        }

        // PUT: api/VehicleSurveillance/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveillanceVehicle(long id, SurveillanceVehicle surveillanceVehicle)
        {
            if (id != surveillanceVehicle.ObservationId)
            {
                return BadRequest();
            }

            _context.Entry(surveillanceVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveillanceVehicleExists(id))
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

        // POST: api/VehicleSurveillance
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SurveillanceVehicle>> PostSurveillanceVehicle(SurveillanceVehicle surveillanceVehicle)
        {
            _context.SurveillanceVehicle.Add(surveillanceVehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveillanceVehicle", new { id = surveillanceVehicle.ObservationId }, surveillanceVehicle);
        }

        // DELETE: api/VehicleSurveillance/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SurveillanceVehicle>> DeleteSurveillanceVehicle(long id)
        {
            var surveillanceVehicle = await _context.SurveillanceVehicle.FindAsync(id);
            if (surveillanceVehicle == null)
            {
                return NotFound();
            }

            _context.SurveillanceVehicle.Remove(surveillanceVehicle);
            await _context.SaveChangesAsync();

            return surveillanceVehicle;
        }

        private bool SurveillanceVehicleExists(long id)
        {
            return _context.SurveillanceVehicle.Any(e => e.ObservationId == id);
        }
    }
}
