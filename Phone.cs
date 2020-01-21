using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CsiApi
{
    public partial class Phone
    {
        public long? PhoneId { get; set; }
        //public long? AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneProvider { get; set; }
        public string SubscriberName { get; set; }
        public string SubscriberAddress { get; set; }

        //public virtual PersonPhone PersonPhone {get;set;}

        [NotMapped]
        public string RandomPhoneNumber  
        { 
            get 
            {
             
                var formatString = "{0}-{1}-{2}";
                var sequence = Enumerable.Range(1,9).OrderBy( n => n * n * ( new Random() ).Next() );
                var s1 = sequence.Distinct().Take(3); 
                var s2 = sequence.Distinct().Take(3); 
                var s3 = sequence.Distinct().Take(4); 
                var str1 = ""; 
                var str2 = ""; 
                var str3 = ""; 

                foreach(var s in s1)
                    str1 += s; 

                foreach(var s in s2)
                    str2 += s; 

                foreach(var s in s3)
                    str3 += s; 

                return string.Format(formatString,str1, str2, str3);
            }
        }

    }
}
