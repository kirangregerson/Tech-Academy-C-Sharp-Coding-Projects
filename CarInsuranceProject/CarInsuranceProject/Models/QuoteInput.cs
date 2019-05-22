using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsuranceProject.Models
{
    public class QuoteInput
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string DateOfBirth { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public bool HasDUI { get; set; }
        public int NumberOfTickets { get; set; }
        public bool FullCoverage { get; set; }
    }
}