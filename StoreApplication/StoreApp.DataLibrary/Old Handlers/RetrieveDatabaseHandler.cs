using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.ConnectionData;
using StoreApp.DataLibrary.Entities;
using System;
using System.Collections.Generic;

namespace StoreApp.DataLibrary.Handlers
{
    public class RetrieveDatabaseHandler
    {
        private readonly StoreApplicationContext _context;
        public RetrieveDatabaseHandler(StoreApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of customer data using an inputted customerID and a Database context
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public StoreApp.BusinessLogic.Objects.Customer GetCustomerDataFromID(int customerID)
        {
            //Some code to retrieve a list of customer data

            try
            {
                foreach (Entities.Customer cust in _context.Customer)
                {
                    if (cust.CustomerId == customerID)
                    {
                        return ParseHandler.ContextCustomerToLogicCustomer(cust);
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
        /// <returns></returns>
        public List<StoreApp.BusinessLogic.Objects.Customer> GetAllCustomerData()
        {
            List<StoreApp.BusinessLogic.Objects.Customer> listOfCustomerData = new List<StoreApp.BusinessLogic.Objects.Customer>();

            foreach (StoreApp.DataLibrary.Entities.Customer customerInDB in _context.Customer)
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
        /// <returns></returns>
        public BusinessLogic.Objects.Manager GetManagerDataFromID(int managerID)
        {

            try
            {
                foreach (StoreApp.DataLibrary.Entities.Manager man in _context.Manager)
                {
                    if (man.ManagerId == managerID)
                    {
                        return ParseHandler.ContextManagerToLogicManager(man);
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
        /// <returns></returns>
        public BusinessLogic.Objects.Store GetStoreFromStoreNumber(int storeNum)
        {
            BusinessLogic.Objects.Store BLStore = new BusinessLogic.Objects.Store();
            try
            {
                foreach (StoreApp.DataLibrary.Entities.Store storeLoc in _context.Store)
                {
                    if (storeLoc.StoreNumber == storeNum)
                    {
                        BLStore = ParseHandler.ContextStoreToLogicStore(storeLoc);
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
        /// <returns></returns>
        public List<StoreApp.BusinessLogic.Objects.Order> GetListOfOrdersByCustomerID(int custID)
        {
            List<Order> listToBeReturned = new List<Order>();

            try
            {
                foreach (Entities.Orders CTXOrder in _context.Orders)
                {
                    if (CTXOrder.CustomerId == custID)
                    {
                        listToBeReturned.Add(ParseHandler.ContextOrderToLogicOrder(CTXOrder));
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
        /// <returns></returns>
        public int GetNewCustomerID()
        {
            int NewCustID = 0;
            foreach (Entities.Customer cust in _context.Customer)
            {
                NewCustID = cust.CustomerId;
            }
            return NewCustID;
        }

        /// <summary>
        /// Gets the inventory of a store given a store number and a database context
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <returns></returns>
        public Inventory GetStoreInventoryByStoreNumber(int storeNumber)
        {
            Inventory BLInventory = new Inventory();
            BusinessLogic.Objects.Product BLProduct = new BusinessLogic.Objects.Product();
            try
            {
                foreach (Entities.InventoryProduct prod in _context.InventoryProduct)
                {
                    if (prod.StoreNumber == storeNumber)
                    {
                        BLProduct = ParseHandler.ContextInventoryProductToLogicProduct(prod);
                        BLInventory.productData.Add(BLProduct);
                        BLProduct = new BusinessLogic.Objects.Product();
                    }
                    else
                    {

                    }
                }
                foreach (BusinessLogic.Objects.Product prod in BLInventory.productData)
                {
                    foreach (Entities.Product entProd in _context.Product)
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
        /// <returns></returns>
        public List<BusinessLogic.Objects.Product> GetListOrderProductByOrderID(Order BLOrder)
        {
            List<BusinessLogic.Objects.Product> BLProdList = new List<BusinessLogic.Objects.Product>();

            foreach (Entities.OrderProduct CTXOrdProd in _context.OrderProduct)
            {
                if (CTXOrdProd.OrderId == BLOrder.orderID)
                {
                    BLProdList.Add(ParseHandler.ContextOrderProductToLogicProduct(CTXOrdProd));
                }
            }
            foreach (BusinessLogic.Objects.Product BLProd in BLProdList)
            {
                foreach (Entities.Product CTXProd in _context.Product)
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
        /// <returns></returns>
        public BusinessLogic.Objects.Store GetStoreInformationFromOrderNumber(int orderID)
        {
            BusinessLogic.Objects.Store BLStore = new BusinessLogic.Objects.Store();

            try
            {
                foreach (Entities.OrderProduct CTXOrdProd in _context.OrderProduct)
                {
                    if (CTXOrdProd.OrderId == orderID)
                    {
                        BLStore.storeNumber = CTXOrdProd.StoreNumber;
                    }
                }
                foreach (Entities.Store CTXStore in _context.Store)
                {
                    if (CTXStore.StoreNumber == BLStore.storeNumber)
                    {
                        BLStore = ParseHandler.ContextStoreToLogicStore(CTXStore);
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

        public List<Order> GetListOfOrdersFromStoreNumber(int storeNumber)
        {
            List<BusinessLogic.Objects.Order> BLListOrders = new List<Order>();

            foreach (Entities.Orders CTXOrder in _context.Orders)
            {
                BLListOrders.Add(ParseHandler.ContextOrderToLogicOrder(CTXOrder));
            }
            foreach (BusinessLogic.Objects.Order BLOrder in BLListOrders)
            {
                foreach (Entities.OrderProduct CTXOrdProd in _context.OrderProduct)
                {
                    if (BLOrder.orderID == CTXOrdProd.OrderId)
                    {
                        BLOrder.customerProductList.Add(ParseHandler.ContextOrderProductToLogicProduct(CTXOrdProd));
                    }
                }
            }
            return BLListOrders;
        }
    }
}