using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsiApi
{
    public partial class PersonPhone
    {
        public long PersonId { get; set; }

        //[ForeignKey("Phone")]
        public long PhoneId { get; set; }

        [NotMapped]
        //[ForeignKey("PhoneId")]
        public virtual Phone Phone {get;set;}
    }
}
