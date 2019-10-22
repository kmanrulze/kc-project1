using Microsoft.AspNetCore.Mvc;
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
        [TempData]
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [DisplayName("Products")]
        public List<Product> CustomerProduct { get; set; }
        [DisplayName("Order ID")]
        public List<int> CustomerOrderIDs { get; set; }
        [DisplayName("Store Location Number")]
        public List<int> OrderStore { get; set; }
        public List<Order> CustomerOrders { get; set; }
    }
}
