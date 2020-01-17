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
    public class PersonSurveillanceController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public PersonSurveillanceController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/PersonSurveillance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveillancePerson>>> GetSurveillancePerson()
        {
            return await _context.SurveillancePerson.ToListAsync();
        }

        // GET: api/PersonSurveillance/5
        [HttpGet("{id}")]
        public ActionResult<List<SurveillancePerson>> GetSurveillancePerson(long id)
        {
            var surveillancePerson =  _context.SurveillancePerson.Where(sp => sp.PersonId == id);

            if (surveillancePerson == null)
            {
                return NotFound();
            }

            foreach(var result in surveillancePerson)
            {
                result.Observation = _context.SurveillanceObservation.SingleOrDefault(o => o.ObservationId == result.ObservationId);
            }

            return Ok(surveillancePerson);
        }

        // PUT: api/PersonSurveillance/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveillancePerson(long id, SurveillancePerson surveillancePerson)
        {
            if (id != surveillancePerson.ObservationId)
            {
                return BadRequest();
            }

            _context.Entry(surveillancePerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveillancePersonExists(id))
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

        // POST: api/PersonSurveillance
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SurveillancePerson>> PostSurveillancePerson(SurveillancePerson surveillancePerson)
        {
            _context.SurveillancePerson.Add(surveillancePerson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveillancePerson", new { id = surveillancePerson.ObservationId }, surveillancePerson);
        }

        // DELETE: api/PersonSurveillance/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SurveillancePerson>> DeleteSurveillancePerson(long id)
        {
            var surveillancePerson = await _context.SurveillancePerson.FindAsync(id);
            if (surveillancePerson == null)
            {
                return NotFound();
            }

            _context.SurveillancePerson.Remove(surveillancePerson);
            await _context.SaveChangesAsync();

            return surveillancePerson;
        }

        private bool SurveillancePersonExists(long id)
        {
            return _context.SurveillancePerson.Any(e => e.ObservationId == id);
        }
    }
}
