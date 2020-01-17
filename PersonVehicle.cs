using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class PersonVehicle
    {
        public long PersonId { get; set; }
        public long VehicleId { get; set; }

        [NotMapped]
        public virtual Vehicle Vehicle {get;set;}
    }
}
