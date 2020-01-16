using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class SurveillanceObservation
    {
        public long? ObservationId { get; set; }
        public long? DocumentId { get; set; }
        public string ObservationText { get; set; }
        public byte[] ObservationDateTime { get; set; }
        public string ObservationStreetAddress { get; set; }
        public string ObservationCity { get; set; }
        public string ObservationProvince { get; set; }
        public byte[] ObservationLatitude { get; set; }
        public byte[] ObservationLongitude { get; set; }
    }
}
