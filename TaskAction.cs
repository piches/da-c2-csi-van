using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class TaskAction
    {
        public long? ActionId { get; set; }
        public long? TaskId { get; set; }
        public long? ActionNumber { get; set; }
        public string ActionType { get; set; }
        public string ActionDescription { get; set; }
        public byte[] ActionCompletedDateTime { get; set; }
    }
}
