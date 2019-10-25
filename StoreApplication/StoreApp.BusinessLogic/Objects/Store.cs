namespace StoreApp.BusinessLogic.Objects
{
    public class Store
    {
        private Address address = new Address();
        private Inventory storeInventory = new Inventory();

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }
        public Inventory StoreInventory
        {
            get { return storeInventory; }
            set { storeInventory = value; }
        }

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
