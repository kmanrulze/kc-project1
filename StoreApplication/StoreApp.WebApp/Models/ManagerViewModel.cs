using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class ManagerViewModel
    {
        [Required]
        [TempData]
        [DisplayName("Manager ID")]
        public int ManagerID { get; set; }
        public int StoreID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Street")]
        public string StoreStreet { get; set; }
        [DisplayName("City")]
        public string StoreCity { get; set; }
        [DisplayName("State")]
        public string StoreState { get; set; }
        [DisplayName("Zip")]
        public string StoreZip { get; set; }
    }
}
