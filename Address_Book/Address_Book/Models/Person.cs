


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Display(Name = "Organization")]
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }

    }
}
