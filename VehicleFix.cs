using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class VehicleFix
    {
        public long VehicleId { get; set; }
        public long? DocumentId { get; set; }
        public long? FixId { get; set; }
        public string VehicleFixDateTime { get; set; }
        public double? VehicleLatitude { get; set; }
        public double? VehicleLongitude { get; set; }

      
    }
}
