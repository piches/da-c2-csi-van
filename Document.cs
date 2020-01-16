using System;
using System.Collections.Generic;

namespace CsiApi
{
    public partial class Document
    {
        public long? DocumentId { get; set; }
        public long? ActionId { get; set; }
        public string DocumentType { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }
}
