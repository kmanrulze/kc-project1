using Microsoft.EntityFrameworkCore;
using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.ConnectionData;
using StoreApp.DataLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.DataLibrary.Handlers
{
    public class RetrieveDatabaseHandler
    {
        private ParseHandler parser = new ParseHandler();

        /// <summary>
        /// Gets a list of customer data using an inputted customerID and a Database context
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public StoreApp.BusinessLogic.Objects.Customer GetCustomerDataFromID(int customerID, StoreApplicationContext context)
        {
            //Some code to retrieve a list of customer data

            try
            {
                foreach (Entities.Customer cust in context.Customer)
                {
                    if (cust.CustomerId == customerID)
                    {
                        return parser.ContextCustomerToLogicCustomer(cust);
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Operation failed: " + e.Message);
                return null;
            }            
        }

        /// <summary>
        /// Returns the connection string
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return Secret.connectionString;
        }

        /// <summary>
        /// Given a context, it returns all the customer data in the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<StoreApp.BusinessLogic.Objects.Customer> GetAllCustomerData(StoreApplicationContext context)
        {
            List<StoreApp.BusinessLogic.Objects.Customer> listOfCustomerData = new List<StoreApp.BusinessLogic.Objects.Customer>();

            foreach (StoreApp.DataLibrary.Entities.Customer customerInDB in context.Customer)
            {
                StoreApp.BusinessLogic.Objects.Customer retrievedCustomer = new StoreApp.BusinessLogic.Objects.Customer();

                listOfCustomerData.Add(retrievedCustomer);
            }

            return listOfCustomerData;
        }

        /// <summary>
        /// Given a manager ID and a database context, returns manager data into a BusinessLibrary manager object
        /// </summary>
        /// <param name="managerID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public BusinessLogic.Objects.Manager GetManagerDataFromID(int managerID, StoreApplicationContext context)
        {

            try
            {
                foreach (StoreApp.DataLibrary.Entities.Manager man in context.Manager)
                {
                    if (man.ManagerId == managerID)
                    {
                        return parser.ContextManagerToLogicManager(man);
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Operation failed: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a business logic version of a store given a store number and a Database context
        /// </summary>
        /// <param name="storeNum"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public BusinessLogic.Objects.Store GetStoreFromStoreNumber(int storeNum, StoreApplicationContext context)
        {
            BusinessLogic.Objects.Store BLStore = new BusinessLogic.Objects.Store();
            try
            {
                foreach (StoreApp.DataLibrary.Entities.Store storeLoc in context.Store)
                {
                    if (storeLoc.StoreNumber == storeNum)
                    {
                        BLStore = parser.ContextStoreToLogicStore(storeLoc);
                    }
                }
                return BLStore;
            }
            catch (Exception e)
            {
                Console.WriteLine("Operation failed: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a list of Business Logic orders under a valid customer ID and a database context
        /// </summary>
        /// <param name="custID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<StoreApp.BusinessLogic.Objects.Order> GetListOfOrdersByCustomerID(int custID, StoreApplicationContext context)
        {
            List<Order> listToBeReturned = new List<Order>();

            try
            {
                foreach (Entities.Orders CTXOrder in context.Orders)
                {
                    if (CTXOrder.CustomerId == custID)
                    {
                        listToBeReturned.Add(parser.ContextOrderToLogicOrder(CTXOrder));
                    }
                }
                return listToBeReturned;
            }
            catch (Exception e)
            {
                Console.WriteLine("Operation failed: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets the customer ID of a newly created customer
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int GetNewCustomerID(StoreApplicationContext context)
        {
            int NewCustID = 0;
            foreach (Entities.Customer cust in context.Customer)
            {
                NewCustID = cust.CustomerId;
            }
            return NewCustID;
        }

        /// <summary>
        /// Gets the inventory of a store given a store number and a database context
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Inventory GetStoreInventoryByStoreNumber(int storeNumber, StoreApplicationContext context)
        {
            Inventory BLInventory = new Inventory();
            BusinessLogic.Objects.Product BLProduct = new BusinessLogic.Objects.Product();
            try
            {
                foreach (Entities.InventoryProduct prod in context.InventoryProduct)
                {
                    if (prod.StoreNumber == storeNumber)
                    {
                        BLProduct = parser.ContextInventoryProductToLogicProduct(prod);
                        BLInventory.productData.Add(BLProduct);
                        BLProduct = new BusinessLogic.Objects.Product();
                    }
                    else
                    {

                    }
                }
                foreach (BusinessLogic.Objects.Product prod in BLInventory.productData)
                {
                    foreach (Entities.Product entProd in context.Product)
                    {
                        if (prod.productTypeID == entProd.ProductTypeId)
                        {
                            prod.name = entProd.ProductName;
                        }
                    }
                }
                return BLInventory;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to get the list of store inventory items: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a list of BusinessLogic products using a BusinessLogic Order and a database context
        /// </summary>
        /// <param name="BLOrder"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<BusinessLogic.Objects.Product> GetListOrderProductByOrderID(Order BLOrder, StoreApplicationContext context)
        {
            List<BusinessLogic.Objects.Product> BLProdList = new List<BusinessLogic.Objects.Product>();

            foreach (Entities.OrderProduct CTXOrdProd in context.OrderProduct)
            {
                if (CTXOrdProd.OrderId == BLOrder.orderID)
                {
                    BLProdList.Add(parser.ContextOrderProductToLogicProduct(CTXOrdProd));
                }
            }
            foreach (BusinessLogic.Objects.Product BLProd in BLProdList)
            {
                foreach (Entities.Product CTXProd in context.Product)
                {
                    //If the product in the list is equal to a product ID on the product table, parse to fill name
                    if (BLProd.productTypeID == CTXProd.ProductTypeId)
                    {
                        BLProd.name = CTXProd.ProductName;
                    }
                }
            }
            return BLProdList;
        }
        /// <summary>
        /// Gets the information of a business logic store given an orderID that it came from and a database context
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public BusinessLogic.Objects.Store GetStoreInformationFromOrderNumber (int orderID, StoreApplicationContext context)
        {
            BusinessLogic.Objects.Store BLStore = new BusinessLogic.Objects.Store();

            try
            {
                foreach (Entities.OrderProduct CTXOrdProd in context.OrderProduct)
                {
                    if (CTXOrdProd.OrderId == orderID)
                    {
                        BLStore.storeNumber = CTXOrdProd.StoreNumber;
                    }
                }
                foreach (Entities.Store CTXStore in context.Store)
                {
                    if (CTXStore.StoreNumber == BLStore.storeNumber)
                    {
                        BLStore = parser.ContextStoreToLogicStore(CTXStore);
                    }
                }
                return BLStore;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to get store information from order: " + e.Message);
                return null;
            }
        }

        public List<Order> GetListOfOrdersFromStoreNumber(int storeNumber, StoreApplicationContext context)
        {
            List<BusinessLogic.Objects.Order> BLListOrders = new List<Order>();

            foreach (Entities.Orders CTXOrder in context.Orders)
            {
                BLListOrders.Add(parser.ContextOrderToLogicOrder(CTXOrder));
            }
            foreach (BusinessLogic.Objects.Order BLOrder in BLListOrders)
            {
                foreach (Entities.OrderProduct CTXOrdProd in context.OrderProduct)
                {
                    if (BLOrder.orderID == CTXOrdProd.OrderId)
                    {
                        BLOrder.customerProductList.Add(parser.ContextOrderProductToLogicProduct(CTXOrdProd));
                    }
                }
            }
            return BLListOrders;
        }
    }
}