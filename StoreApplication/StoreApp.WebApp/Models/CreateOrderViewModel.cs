using StoreApp.BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.WebApp.Models
{
    public class CreateOrderViewModel
    {
        public List<Product> Products { get; set; }
        //public List<decimal> ProdPrice { get; set; }
        //public List<string> ProdProductNames { get; set; }

    }

}
