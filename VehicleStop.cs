using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class VehicleStop
    {
        [Key]
        public long? VehicleStopId { get; set; }
        public long? VehicleId { get; set; }
        public long? DocumentId { get; set; }
        public long? VehicleStopNumber { get; set; }
        public int VehicleStopStartDateTime { get; set; }
        public int VehicleStopEndDateTime { get; set; }
        public double? VehicleLatitude { get; set; }
        public double? VehicleLongitude { get; set; }
        public long? VehicleStopDuration { get; set; }

        [NotMapped]
        public long? TargetNumber { get; set; }

        public DateTime StopStart
        {
            get
            {
                System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds( VehicleStopStartDateTime).ToLocalTime();
                return dtDateTime;
            }
        }

        public DateTime StopEnd
        {
            get
            {
                System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds( VehicleStopEndDateTime).ToLocalTime();
                return dtDateTime;
            }
        }
    }
}
