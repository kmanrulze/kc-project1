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
        public int ManagerID { get; set; }
        public int StoreID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StoreStreet { get; set; }
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public string StoreZip { get; set; }
    }
}
