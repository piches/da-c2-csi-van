using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CsiApi
{
    public partial class Investigation
    {
        [Key]
        public long? CaseId { get; set; }
        public string CaseName { get; set; }

        public string FileNumber { get; set; }
        public string PrimaryInvestigator { get; set; }
        public byte[] CaseDateTime { get; set; }
    }
}
