using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class Person
    {
        [Key]
        public long? PersonId { get; set; }
        public long? TargetNumber { get; set; }
        public string Surname { get; set; }
        public string GivenName1 { get; set; }
        public string GivenName2 { get; set; }
        public int? DateOfBirth { get; set; }
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

        public DateTime DOB 
        {
            get
            {
                if(DateOfBirth.HasValue)
                {
                    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
                    dtDateTime = dtDateTime.AddSeconds( DateOfBirth.Value ).ToLocalTime();
                    return dtDateTime;
                }
                else
                {
                    return default(DateTime);
                }
            }
        }
        [NotMapped]
        public ICollection<Phone> Phones { get; set;}

        [NotMapped]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
