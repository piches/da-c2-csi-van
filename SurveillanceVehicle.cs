using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class SurveillanceVehicle
    {
        [Key]
        public long? ObservationId { get; set; }
        public long? VehicleId { get; set; }

        [NotMapped]
        public virtual SurveillanceObservation Observation { get;set;}
    }
}
