using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class Person
    {
        public long? PersonId { get; set; }
        public long? TargetNumber { get; set; }
        public string Surname { get; set; }
        public string GivenName1 { get; set; }
        public string GivenName2 { get; set; }
        public byte[] DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Race { get; set; }
        public string HairColour { get; set; }
        public string EyeColour { get; set; }
        public string PersonHeight { get; set; }
        public string PersonWeight { get; set; }
        public string HomeStreetAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeProvince { get; set; }
        public string DriversLicenceNumber { get; set; }
        public string ProvinceOfInsurance { get; set; }
        public string PersonRole { get; set; }
        public string PersonImageLink { get; set; }
    }
}
