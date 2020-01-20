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

    public class Fix 
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public DateTime Time { get; set;}
    }

    public class PersonCoordinate
    {
        public Person Target { get;set; }
        public List<Coordinate<VehicleStop>> TrackingData { get; set; }
        public List<Coordinate<PhoneCall>> CellphoneData { get; set;}
        public List<Coordinate<SurveillanceObservation>> SurveillanceData { get; set; }

        public List<Fix> Fixes { 
            get
            {
                var list = new List<Fix>();

                foreach(var r in CellphoneData)
                {
                    if(r.Long.HasValue && r.Lat.HasValue && r.Time.HasValue)
                        list.Add(new Fix() { Time = r.Time.Value, Long = r.Long.Value, Lat = r.Lat.Value });
                }

                foreach(var r in TrackingData)
                {
                    if(r.Long.HasValue && r.Lat.HasValue && r.Time.HasValue)
                        list.Add(new Fix() { Time = r.Time.Value, Long = r.Long.Value, Lat = r.Lat.Value });
                }

                foreach(var r in SurveillanceData)
                {
                    if(r.Long.HasValue && r.Lat.HasValue && r.Time.HasValue)
                        list.Add(new Fix() { Time = r.Time.Value, Long = r.Long.Value, Lat = r.Lat.Value });
                }

                list.OrderBy(r => r.Time);

                return list;
            }
        }

        public PersonCoordinate(Person target)
        {
            if(target == null)
                throw new ArgumentOutOfRangeException("target"); 

            Target = target;

            TrackingData = new List<Coordinate<VehicleStop>>();
            CellphoneData = new List<Coordinate<PhoneCall>>();
            SurveillanceData = new List<Coordinate<SurveillanceObservation>>();

            
        }   
    }

    public enum DataSourceType
    {
        PhoneRecord = 1, 
        TrackerData = 2, 
        SurveillanceObservation = 3
    }

    public class Coordinate<T> 
    {
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public DateTime? Time { get; set; }  
        public DataSourceType SourceType { get; set; }

        public T Source { get; set;}

        public Coordinate(T source, DataSourceType sourceType)
        {
            if(source == null)
                throw new ArgumentOutOfRangeException("source");

            Source = source;
            SourceType = sourceType;
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {
        private readonly db_CSIContext _context;

        public CoordinateController(db_CSIContext context)
        {
            _context = context;
        }
        
         [HttpGet("{id}")]
        public ActionResult<PersonCoordinate> GetCoordinate(long? id)
        {
            var person = _context.Person.Find(id);

            if(person == null)
                return NotFound(); 

            var pc = new PersonCoordinate(person);
            
            var vehicles = _context.PersonVehicle.Where(pv => pv.PersonId == person.PersonId);

            foreach(var v in vehicles)
            {
                if(v != null)
                {
                    var stops =  _context.VehicleStop.Where(vs => vs.VehicleId == v.VehicleId && vs.VehicleLatitude.HasValue && vs.VehicleLongitude.HasValue);//.Take(100);
                    foreach(var stop in stops)
                    {
                        pc.TrackingData.Add( new Coordinate<VehicleStop>(stop, DataSourceType.TrackerData){
                            Lat = stop.VehicleLatitude, 
                            Long = stop.VehicleLongitude,
                            Time = stop.StopStart
                        });
                    }
                }
            }

            var observations = _context.SurveillancePerson.Where(sp => sp.PersonId == person.PersonId).Select(sp => sp.ObservationId);
            
            foreach(var observationId in observations)
            {
                var so = _context.SurveillanceObservation.SingleOrDefault(s => s.ObservationId == observationId);

                if(so.ObservationLongitude.HasValue && so.ObservationLatitude.HasValue)
                    pc.SurveillanceData.Add(new Coordinate<SurveillanceObservation>(so, DataSourceType.SurveillanceObservation)
                    {
                        Time = so.ObservationDate, 
                        Lat = so.ObservationLatitude, 
                        Long = so.ObservationLongitude
                    });
            }
            
            var phoneIds =  _context.PersonPhone.Where(pp => pp.PersonId == person.PersonId).Select(pp => pp.PhoneId);

            foreach(var phoneId in phoneIds)
            {
                var phoneRecords = _context.PhoneCall.Where(pCall => pCall.TargetPhoneId == phoneId);
                foreach(var record in phoneRecords)
                {
                    
                    if(record.StartCellTowerLatitude.HasValue && record.StartCellTowerLongitude.HasValue)
                    {
                        pc.CellphoneData.Add(new Coordinate<PhoneCall>(record, DataSourceType.PhoneRecord)
                        {
                            Time = record.CallStartTime, 
                            Lat = record.StartCellTowerLatitude, 
                            Long = record.StartCellTowerLongitude
                        });

                    }
                }

            }

            return Ok(pc);
        }

    }
}