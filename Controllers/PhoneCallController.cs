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
    public class PhoneCallController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public PhoneCallController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/PhoneCall
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneCall>>> GetPhoneCall()
        {
            return await _context.PhoneCall.ToListAsync();
        }

        // GET: api/PhoneCall/5
        [HttpGet("{id}")]
        public ActionResult<List<PhoneCall>> GetPhoneCall(long id)
        {

            var phoneIds = _context.PersonPhone.Where(pp => pp.PersonId == id).Select(pp => pp.PhoneId);

            if(!phoneIds.Any())
                return NotFound();
            
            var calls = new List<PhoneCall>();
            foreach(var phoneId in phoneIds)
            {
                var callsForPhone = _context.PhoneCall.Where(pc => pc.TargetPhoneId == phoneId);
                calls.AddRange(callsForPhone);
            }

            return Ok(calls);
        }

        // PUT: api/PhoneCall/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoneCall(long id, PhoneCall phoneCall)
        {
            if (id != phoneCall.PhoneCallId)
            {
                return BadRequest();
            }

            _context.Entry(phoneCall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneCallExists(id))
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

        // POST: api/PhoneCall
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PhoneCall>> PostPhoneCall(PhoneCall phoneCall)
        {
            _context.PhoneCall.Add(phoneCall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneCall", new { id = phoneCall.PhoneCallId }, phoneCall);
        }

        // DELETE: api/PhoneCall/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhoneCall>> DeletePhoneCall(long id)
        {
            var phoneCall = await _context.PhoneCall.FindAsync(id);
            if (phoneCall == null)
            {
                return NotFound();
            }

            _context.PhoneCall.Remove(phoneCall);
            await _context.SaveChangesAsync();

            return phoneCall;
        }

        private bool PhoneCallExists(long id)
        {
            return _context.PhoneCall.Any(e => e.PhoneCallId == id);
        }
    }
}
