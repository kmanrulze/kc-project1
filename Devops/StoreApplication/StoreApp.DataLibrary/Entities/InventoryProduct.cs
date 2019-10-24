using System;
using System.Collections.Generic;

namespace StoreApp.DataLibrary.Entities
{
    public partial class InventoryProduct
    {
        public int InventoryProductId { get; set; }
        public int StoreNumber { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductAmount { get; set; }

        public virtual Product ProductType { get; set; }
        public virtual Store StoreNumberNavigation { get; set; }
    }
}
