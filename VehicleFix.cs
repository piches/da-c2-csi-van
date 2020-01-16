using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class VehicleFix
    {
        public long? VehicleFixId { get; set; }
        public long? VehicleId { get; set; }
        public long? DocumentId { get; set; }
        public long? FixId { get; set; }
        public byte[] VehicleFixDateTime { get; set; }
        public byte[] VehicleLatitude { get; set; }
        public byte[] VehicleLongitude { get; set; }
    }
}
