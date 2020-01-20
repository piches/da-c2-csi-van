using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsiApi;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace CsiApi.Controllers
{

    public class MonitorOptions : IOptionsMonitor<ConsoleLoggerOptions>
    {
        public ConsoleLoggerOptions CurrentValue => new ConsoleLoggerOptions();

        public ConsoleLoggerOptions Get(string name)
        {
            return CurrentValue;
        }

        public IDisposable OnChange(Action<ConsoleLoggerOptions, string> listener)
        {
            return null;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly db_CSIContext _context;

        private static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new ConsoleLoggerProvider(new MonitorOptions())
        });

        public static ILoggerFactory LoggerFactory => loggerFactory;

        public VehicleController(db_CSIContext context)
        {
            _context = context;
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicle()
        {
            return await _context.Vehicle.ToListAsync();
        }

        // GET: api/Vehicle/5
      

         // GET: api/Vehicle/123ABC
        [HttpGet("{plate}")]
        //[ActionName("FindByPlate")]
        public async Task<ActionResult<Vehicle>> FindByPlate(string plate)
        {
            if(plate == null)
                return NotFound(); 

            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.VehicleLicensePlate.ToLower() == plate.ToLower());

            if(vehicle != null)
            {
                var stops =  _context.VehicleStop.Where(vf => vf.VehicleId == vehicle.VehicleId);
                var stopCollection = new List<VehicleStop>();
                foreach(var f in stops)
                    stopCollection.Add(f); 
                    
                vehicle.Stops = stopCollection;
            }

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicle/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(long? id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicle
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(long? id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(long? id)
        {
            return _context.Vehicle.Any(e => e.VehicleId == id);
        }
    }
}
