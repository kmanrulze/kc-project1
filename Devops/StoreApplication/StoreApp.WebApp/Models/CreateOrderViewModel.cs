using StoreApp.BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.WebApp.Models
{
    public class CreateOrderViewModel
    {
        public List<RequestedProducts> Products { get; set; }
        //public List<decimal> ProdPrice { get; set; }
        //public List<string> ProdProductNames { get; set; }

    }
    public class RequestedProducts
    {
        public int ProductID { get; set; }
        [DisplayName("Order Amount Wanted")]
        public int ProductAmount { get; set; }
        [DisplayName("Name of Product")]
        public string ProductName { get; set; }
        [DisplayName("Price")]
        public decimal ProductPrice { get; set; }
    }

}
