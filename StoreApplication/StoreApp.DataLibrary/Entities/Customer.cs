using System;
using System.Collections.Generic;

namespace StoreApp.DataLibrary.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
