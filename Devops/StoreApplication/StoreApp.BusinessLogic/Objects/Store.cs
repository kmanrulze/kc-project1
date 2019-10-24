using System;
using System.Collections.Generic;
using System.Text;


namespace StoreApp.BusinessLogic.Objects
{
    public class Store
    {
        public Address address = new Address();
        public Inventory storeInventory = new Inventory();
        public int storeNumber { get; set; }
        public bool CheckInventory(Store locationBeingChecked, Order order)
        {
            //Some code to check store inventory

            return false;
        }
        public void UpdateInventory(Store locationToBeUpdated, Order placedOrder)
        {
            //Some code to update inventory
        }
        public bool CheckStoreNotNull()
        {
            if (this.storeNumber >0 && this.address.CheckAddressNotNull() == true)
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
