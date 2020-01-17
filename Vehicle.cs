using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CsiApi
{
    public partial class Vehicle
    {
        [Key]
        public long? VehicleId { get; set; }
        public string VehicleProvince { get; set; }
        public string VehicleLicensePlate { get; set; }
        public string VehicleVin { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColour { get; set; }
        public long? VehicleYear { get; set; }
        public string VehicleRegisteredOwner { get; set; }

        [NotMapped]
        public ICollection<VehicleFix> Fixes {get;set;}
    }
}
