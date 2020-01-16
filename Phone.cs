using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class Phone
    {
        public long? PhoneId { get; set; }
        public long? AreaCode { get; set; }
        public long? PhoneNumber { get; set; }
        public string PhoneProvider { get; set; }
        public string SubscriberName { get; set; }
        public string SubsriberAddress { get; set; }
    }
}
