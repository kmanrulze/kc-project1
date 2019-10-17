
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BusinessLogic.Objects
{
    public class Customer
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public Address customerAddress = new Address();
        public int customerID { get; set; }

        public bool CheckCustomerNotNull()
        {
            //doesnt check for customer ID in the event that a new customer is being added
            if (customerAddress.CheckAddressNotNull() == true && this.firstName != null && this.lastName != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
