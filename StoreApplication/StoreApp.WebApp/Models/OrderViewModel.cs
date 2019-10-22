using StoreApp.BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class OrderViewModel
    {
        [DisplayName("Order ID")]
        public int OrderID { get; set; }
        [DisplayName("Store Location Number")]
        public int StoreNumber { get; set; }
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        public List<string> Products { get; set; }
    }
}
