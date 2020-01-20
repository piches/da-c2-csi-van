using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CsiApi
{
    public partial class Task
    {
        [Key]
        public long? TaskId { get; set; }
        public long? CaseId { get; set; }
        public string TaskNumber { get; set; }
        public string TaskType { get; set; }
        public string TaskDescription { get; set; }
        public string TaskAssigneeName { get; set; }
        public int TaskAssignedDateTime { get; set; }
        public int? TaskCompletedDateTime { get; set; }
    }
}
