using System;
using System.Collections.Generic;

namespace StoreApp.DataLibrary.Entities
{
    public partial class Manager
    {
        public int ManagerId { get; set; }
        public int StoreNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Store StoreNumberNavigation { get; set; }
    }
}
