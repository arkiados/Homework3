using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homework3.Db
{
    public partial class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string ContactName { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? ContactPhoneType { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public int ContactAge { get; set; }
        public string? ContactNotes { get; set; }
        public DateTime ContactCreatedDate { get; set; }
    }
}
