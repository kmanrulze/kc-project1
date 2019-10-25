using System.Collections.Generic;


namespace StoreApp.BusinessLogic.Objects
{
    public class Inventory
    {

        private List<Product> productData = new List<Product>();

        public List<Product> ProductData
        {
            get { return productData;}
            set { productData = value; }
        }

        public bool CheckOrderAgainstInventory(List<Product> OrderList)
        {
            bool validOrder = true;
            foreach (Product OProd in OrderList)
            {
                foreach (Product IProd in productData)
                {
                    if (OProd.productTypeID == IProd.productTypeID)
                    {
                        if (OProd.Amount > IProd.Amount)
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