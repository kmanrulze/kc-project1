using StoreApp.BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int StoreNumber { get; set; }
        public Customer CustomerOrderInformation { get; set; }
    }
}
