using System.Collections.Generic;


namespace StoreApp.BusinessLogic.Objects
{
    public class Inventory
    {

        public List<Product> productData = new List<Product>();

        public bool CheckOrderAgainstInventory(List<Product> OrderList)
        {
            bool validOrder = true;
            foreach (Product OProd in OrderList)
            {
                foreach (Product IProd in productData)
                {
                    if (OProd.productTypeID == IProd.productTypeID)
                    {
                        if (OProd.amount > IProd.amount)
                        {
                            validOrder = false;
                        }
                    }
                }
            }
            return validOrder;
        }

        public bool CheckIfProductListNotNull()
        {
            if (productData.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}