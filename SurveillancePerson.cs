using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class SurveillancePerson
    {

        [Key]
        public long ObservationId { get; set; }
        public long PersonId { get; set; }


        [NotMapped]
        public virtual SurveillanceObservation Observation { get;set;}
    }
}
