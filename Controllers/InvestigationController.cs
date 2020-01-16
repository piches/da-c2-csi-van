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
    public class InvestigationController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public InvestigationController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/Investigation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investigation>>> GetInvestigation()
        {
            return await _context.Investigation.ToListAsync();
        }

        // GET: api/Investigation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investigation>> GetInvestigation(long? id)
        {
            var investigation = await _context.Investigation.FindAsync(id);

            if (investigation == null)
            {
                return NotFound();
            }

            return investigation;
        }

        // PUT: api/Investigation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigation(long? id, Investigation investigation)
        {
            if (id != investigation.CaseId)
            {
                return BadRequest();
            }

            _context.Entry(investigation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigationExists(id))
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

        // POST: api/Investigation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Investigation>> PostInvestigation(Investigation investigation)
        {
            _context.Investigation.Add(investigation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvestigation", new { id = investigation.CaseId }, investigation);
        }

        // DELETE: api/Investigation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Investigation>> DeleteInvestigation(long? id)
        {
            var investigation = await _context.Investigation.FindAsync(id);
            if (investigation == null)
            {
                return NotFound();
            }

            _context.Investigation.Remove(investigation);
            await _context.SaveChangesAsync();

            return investigation;
        }

        private bool InvestigationExists(long? id)
        {
            return _context.Investigation.Any(e => e.CaseId == id);
        }
    }
}
