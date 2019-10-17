using System;
using System.Collections.Generic;

namespace StoreApp.DataLibrary.Entities
{
    public partial class Product
    {
        public Product()
        {
            InventoryProduct = new HashSet<InventoryProduct>();
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }

        public virtual ICollection<InventoryProduct> InventoryProduct { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
