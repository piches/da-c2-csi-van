using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CsiApi
{
    public partial class SurveillanceObservation
    {
        [Key]
        public long ObservationId { get; set; }
        public long? DocumentId { get; set; }
        public string ObservationText { get; set; }
        public int? ObservationDateTime { get; set; }
        public string ObservationStreetAddress { get; set; }
        public string ObservationCity { get; set; }
        public string ObservationProvince { get; set; }
        public double? ObservationLatitude { get; set; }
        public double? ObservationLongitude { get; set; }

        public DateTime ObservationDate 
        {
            get
            {
                if(ObservationDateTime.HasValue)
                {
                    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
                    dtDateTime = dtDateTime.AddSeconds( ObservationDateTime.Value ).ToLocalTime();
                    return dtDateTime;
                }
                else
                {
                    return default(DateTime);
                }
            }
        }
    }
}
