using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class CreateCustomerViewModel
    {
        public int ID { get; set; }
        [Required]

        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter a state abbreviation (2 Characters)"),MaxLength(2)]
        public string State { get; set; }
        [Required(ErrorMessage = "Enter a valid zip (5 digits)"),MaxLength(5)]
        public string Zip { get; set; }
    }
}
