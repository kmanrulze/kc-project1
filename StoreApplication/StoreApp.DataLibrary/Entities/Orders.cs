using System;
using System.Collections.Generic;

namespace StoreApp.DataLibrary.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
