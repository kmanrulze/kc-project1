using System.Collections.Generic;

namespace StoreApp.BusinessLogic.Objects
{
    public class Order
    {
        private Store storeLocation = new Store();
        private Customer customer = new Customer();
        private List<Product> customerProductList = new List<Product>();
        public double orderTime { get; set; }
        public int orderID { get; set; }

        public Store StoreLocation
        {
            get { return storeLocation; }
            set { storeLocation = value; }
        }
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public List<Product> CustomerProductList
        {
            get { return customerProductList; }
            set { customerProductList = value; }
        }


        public bool CheckOrderIsValid()
        {
            //check if has any product
            bool goodOrder = true;
            if (customerProductList.Count == 0)
            {
                goodOrder = false;
            }
            //Check if all products dont equal 0
            bool empty = true;
            goodOrder = false;
            while (empty)
            {
                foreach(Product p in customerProductList)
                {
                    if (p.Amount>0)
                    {
                        empty = false;
                        goodOrder = true;
                        break;
                    }
                }
                break;
            }
            foreach(Product p in customerProductList)
            {
                if (p.Amount > 10)
                {
                    goodOrder = false;
                }
            }
            return goodOrder;
        }
        public bool CheckOrderHasIDs()
        {
            if (orderID != 0 && customer.customerID != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CalculateOrderTime(Product customerProductList)
        {
            //Business rule for adding how to calculate the order time

            this.orderTime = 5;
        }
    }
}
