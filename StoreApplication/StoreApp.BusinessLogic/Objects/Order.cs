using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BusinessLogic.Objects
{
    public class Order
    {
        public Store storeLocation = new Store();
        public Customer customer = new Customer();
        public List<Product> customerProductList = new List<Product>();
        public double orderTime { get; set; }
        public int orderID { get; set; }

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
                    if (p.amount>0)
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
                if (p.amount > 10)
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
