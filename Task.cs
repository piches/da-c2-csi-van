using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class Task
    {
        public long? TaskId { get; set; }
        public long? CaseId { get; set; }
        public string TaskNumber { get; set; }
        public string TaskType { get; set; }
        public string TaskDescription { get; set; }
        public string TaskAssigneeName { get; set; }
        public byte[] TaskAssignedDateTime { get; set; }
        public byte[] TaskCompletedDateTime { get; set; }
    }
}
