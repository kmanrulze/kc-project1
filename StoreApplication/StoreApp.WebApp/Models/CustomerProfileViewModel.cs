using StoreApp.BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class CustomerProfileViewModel
    {
        [Required]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public List<Product> CustomerProduct { get; set; }
        public List<int> CustomerOrderIDs { get; set; }
        public List<int> OrderStore { get; set; }
        public List<Order> CustomerOrders { get; set; }
    }
}
