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
    public class SurveillanceController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public SurveillanceController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/Surveillance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveillanceObservation>>> GetSurveillanceObservation()
        {
            return await _context.SurveillanceObservation.ToListAsync();
        }

        // GET: api/Surveillance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SurveillanceObservation>> GetSurveillanceObservation(long id)
        {
            var surveillanceObservation = await _context.SurveillanceObservation.FindAsync(id);

            if (surveillanceObservation == null)
            {
                return NotFound();
            }

            return surveillanceObservation;
        }

        // PUT: api/Surveillance/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveillanceObservation(long id, SurveillanceObservation surveillanceObservation)
        {
            if (id != surveillanceObservation.ObservationId)
            {
                return BadRequest();
            }

            _context.Entry(surveillanceObservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveillanceObservationExists(id))
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

        // POST: api/Surveillance
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SurveillanceObservation>> PostSurveillanceObservation(SurveillanceObservation surveillanceObservation)
        {
            _context.SurveillanceObservation.Add(surveillanceObservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveillanceObservation", new { id = surveillanceObservation.ObservationId }, surveillanceObservation);
        }

        // DELETE: api/Surveillance/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SurveillanceObservation>> DeleteSurveillanceObservation(long id)
        {
            var surveillanceObservation = await _context.SurveillanceObservation.FindAsync(id);
            if (surveillanceObservation == null)
            {
                return NotFound();
            }

            _context.SurveillanceObservation.Remove(surveillanceObservation);
            await _context.SaveChangesAsync();

            return surveillanceObservation;
        }

        private bool SurveillanceObservationExists(long id)
        {
            return _context.SurveillanceObservation.Any(e => e.ObservationId == id);
        }
    }
}
