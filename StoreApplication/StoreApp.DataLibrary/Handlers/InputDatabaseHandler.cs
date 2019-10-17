using Microsoft.EntityFrameworkCore;
using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.ConnectionData;
using StoreApp.DataLibrary.Entities;
using System;
using System.IO;
using System.Linq;

namespace StoreApp.DataLibrary.Handlers
{
    public class InputDatabaseHandler
    {
        private ParseHandler parser = new ParseHandler();

        /// <summary>
        /// Inputs an order into the Order table. Does NOT input products.
        /// </summary>
        /// <param name="BLOrder"></param>
        /// <param name="context"></param>
        public void InputOrder(Order BLOrder, StoreApplicationContext context)
        {

            try
            {
                context.Orders.Add(parser.LogicOrderToContextOrder(BLOrder));
                context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                Console.WriteLine("Something went wrong inputting order: " + e);
            }
        }
        public void ModifyInventoryGivenOrder(Order inputOrder, BusinessLogic.Objects.Store inputStore, StoreApplicationContext context)
        {

        }
        /// <summary>
        /// Inputs the order products into the OrderProduct table under an order ID given a Business Logic Order, an order ID, and a database context
        /// </summary>
        /// <param name="BLOrder"></param>
        /// <param name="orderID"></param>
        /// <param name="context"></param>
        public void InputOrderProduct(BusinessLogic.Objects.Order BLOrder,int orderID, StoreApplicationContext context)
        {
            try
            {
                foreach (BusinessLogic.Objects.Product BLProd in BLOrder.customerProductList)
                {
                    context.OrderProduct.Add(parser.LogicProductToContextOrderProduct(BLOrder, orderID, BLProd));
                }
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong inputting the OrderProduct for the Order: " + e.Message);
                return;
            }
        }

        /// <summary>
        /// Adds new customer data to the Customer database table given a Business Logic customer object and a database context
        /// </summary>
        /// <param name="BLCustomer"></param>
        /// <param name="context"></param>
        public void AddNewCustomerData(StoreApp.BusinessLogic.Objects.Customer BLCustomer, StoreApplicationContext context)
        {
            //Some code to input customer data to the DB

            try
            {
                context.Customer.Add(parser.LogicCustomerToContextCustomer(BLCustomer));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to put the customer into the database: " + e.Message);
            }
        }
        /// <summary>
        /// Deletes a customer from the database
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="context"></param>
        public void DeleteCustomerByID(int customerID, StoreApplicationContext context)
        {
            //Some code that removes customer from the DB given an ID

            //context.Customer.Remove();
        }
        /// <summary>
        /// Returns a connection string
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return Secret.connectionString;
        }
    }
}
