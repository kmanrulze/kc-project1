using System;
using StoreApp.DataLibrary;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Linq;
using StoreApp.BusinessLogic;
using StoreApp.DataLibrary.Handlers;
using System.Collections.Generic;
using StoreApp.DataLibrary.Entities;
using StoreApp.BusinessLogic.Objects;
using NLog;

namespace StoreApp.Main
{
    class Program
    {
        public static RetrieveDatabaseHandler DBRHandler = new RetrieveDatabaseHandler();
        public static InputDatabaseHandler DBIHandler = new InputDatabaseHandler();
        private static readonly ILogger storeLogger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {

            Console.WriteLine("Hello! Welcome to the Revature Resturaunt!");

            string inputOne = "0";
            int n;
            bool isNumeric;

            int menuSwitch = 1;
            // Menu switch guide
            //1 - start menu
            //2 - manager menu
            //3 - customer menu
            //4 - return customer menu
            //5 - new customer menu
            //6 - customer options menu
            //7 - manager store view menu
            //8 - order menu
            //9 - Select store ID


            bool whileInMenu = true;
            bool whileInSecondaryMenu = true;
            BusinessLogic.Objects.Customer retrievedCustomer = new BusinessLogic.Objects.Customer();
            BusinessLogic.Objects.Store retrievedStore = new BusinessLogic.Objects.Store();
            BusinessLogic.Objects.Order inputOrder = new BusinessLogic.Objects.Order();
            List<BusinessLogic.Objects.Order> orderList = new List<BusinessLogic.Objects.Order>();
            StoreApp.BusinessLogic.Objects.Manager retrievedManager = new BusinessLogic.Objects.Manager();

            //DB initialization

            string connectionString = DBRHandler.GetConnectionString();
            DbContextOptions<StoreApplicationContext> options = new DbContextOptionsBuilder<StoreApplicationContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new StoreApplicationContext(options);
            while (whileInMenu)
            {
                string managerIDInput;
                int managerID;
                int customerID;
                int storeNum;

                switch (menuSwitch)
                {
                    case 1: //Start menu
                        while (whileInSecondaryMenu)
                        {
                            Console.WriteLine("Are you using this console as a manager or a customer?\n[1] Manager\n[2] Customer\n");
                            inputOne = CheckAndReturnCustomerOptionChosen(Console.ReadLine(), 2);

                            if (inputOne == "1") //Manager
                            {

                                //code for manager
                                //Can display current stocks and things for locations and other things stored
                                //Managment can stock their stores and check and edit customer data

                                whileInSecondaryMenu = false;
                                menuSwitch = 2;
                            }
                            else if (inputOne == "2") //Customer
                            {
                                //code for customer
                                //Will run code to make new customer, retrieve old customer data, and place orders
                                menuSwitch = 3;
                                whileInSecondaryMenu = false;
                            }
                            else //Invalid input
                            {
                                Console.WriteLine("Invalid input, please type one of the following options");
                            }
                        }
                        whileInSecondaryMenu = true; //resets menu true to go into next menu
                        break;
                    case 2: // manager menu
                        //Some code to compare manager ID to the table and welcome manager options
                        while (whileInSecondaryMenu)
                        {
                            Console.WriteLine("What is your manager ID?");
                            managerIDInput = Console.ReadLine();
                            isNumeric = int.TryParse(managerIDInput, out n);
                            if (isNumeric == false)
                            {
                                Console.WriteLine("Invalid characters. Please try again with a numerical value");
                                break;
                            }
                            else //if the input only has numbers in it
                            {
                                managerID = Int32.Parse(managerIDInput);

                                try
                                {
                                    retrievedManager = DBRHandler.GetManagerDataFromID(managerID, context);
                                    retrievedStore = DBRHandler.GetStoreFromStoreNumber(retrievedManager.storeNumberManaged, context);

                                    Console.WriteLine("Welcome back, " + retrievedManager.firstName + " " + retrievedManager.lastName + "!\nManager of Store Number: " + retrievedManager.storeNumberManaged + "\n");
                                    menuSwitch = 7;
                                    whileInSecondaryMenu = false;
                                    //set case to go to the manager options menu on 7
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("Unable to perform the operation due to null value returned with Customer ID " + managerID + ": " + e.Message + "\n");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Unknown exeption " + e);
                                }
                            }
                        }
                        whileInSecondaryMenu = true; //resets menu true to go into next menu
                        break;
                    case 3: //general customer menu
                        while (whileInSecondaryMenu)
                        {
                            Console.WriteLine("Are you a new customer or a return customer?\n[1] New Customer\n[2] Return Customer\n");
                            inputOne = CheckAndReturnCustomerOptionChosen(Console.ReadLine(), 2);

                            if (inputOne == "1")
                            {
                                whileInSecondaryMenu = false;
                                menuSwitch = 5;
                            }
                            else if (inputOne == "2")
                            {
                                whileInSecondaryMenu = false;
                                menuSwitch = 4;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, please type one of the following options");
                            }
                        }
                        whileInSecondaryMenu = true;
                        break;
                    case 4: //return customer
                        while (whileInSecondaryMenu)
                        {
                            Console.WriteLine("Welcome back! What is your customer ID?");
                            inputOne = Console.ReadLine();
                            
                            isNumeric = int.TryParse(inputOne, out n);

                            if (isNumeric == false)
                            {
                                Console.WriteLine("Invalid characters. Please try again with a numerical value");
                                break;

                            }
                            else //if the input only has numbers in it
                            {

                                customerID = Int32.Parse(inputOne);

                                try
                                {
                                    retrievedCustomer = DBRHandler.GetCustomerDataFromID(customerID, context);
                                    Console.WriteLine("Welcome back, " + retrievedCustomer.firstName + " " + retrievedCustomer.lastName + "! What can we do for you today?");
                                    menuSwitch = 6;
                                    whileInSecondaryMenu = false;
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine("Unable to perform the operation due to null value returned with Customer ID " + customerID + ": " + e.Message + "\n");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Unknown exeption " + e);
                                }
                            }
                        }
                        whileInSecondaryMenu = true; //resets menu true to go into next menu
                        break;
                    case 5: //new customer menu
                        StoreApp.BusinessLogic.Objects.Customer newCust = new StoreApp.BusinessLogic.Objects.Customer();

                        while (whileInSecondaryMenu)
                        {

                            if (newCust.CheckCustomerNotNull() == false)
                            {
                                if (newCust.firstName == null)
                                {
                                    Console.WriteLine("What is your first name?");
                                    newCust.firstName = Console.ReadLine();
                                }
                                else if (newCust.lastName == null)
                                {
                                    Console.WriteLine("What is your last name?");
                                    newCust.lastName = Console.ReadLine();
                                }
                                else if (newCust.customerAddress.CheckAddressNotNull() == false)
                                {
                                    Console.WriteLine("Please enter an address. What is your street?");
                                    newCust.customerAddress.street = Console.ReadLine();

                                    Console.WriteLine("Please enter a city");
                                    newCust.customerAddress.city = Console.ReadLine();

                                    Console.WriteLine("Please enter a state");
                                    newCust.customerAddress.state = Console.ReadLine();

                                    Console.WriteLine("Please enter a zip");
                                    newCust.customerAddress.zip = Console.ReadLine();
                                }
                            }
                            else
                            {
                                try
                                {
                                    Console.WriteLine("Adding profile to database. . .\n");
                                    DBIHandler.AddNewCustomerData(newCust, context);
                                    Console.WriteLine("Customer profile successfully created! Welcome, " + newCust.firstName + "!\n");
                                    int newID = DBRHandler.GetNewCustomerID(context);  //Note, not safe for multiple connections to the DB inputting at once. 

                                    Console.WriteLine("Your new customer ID is: " + newID);

                                    retrievedCustomer = DBRHandler.GetCustomerDataFromID(newID, context);

                                    whileInSecondaryMenu = false;
                                    menuSwitch = 6;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Unknown exception thrown: " + e);
                                }

                            }
                        }
                        whileInSecondaryMenu = true; //resets menu true to go into next menu
                        break;
                    case 6: //customer options menu
                        while(whileInSecondaryMenu)
                        {
                            Console.WriteLine("Customer Options\n[1] Place order\n[2] View profile information\n[3] View order history\n[4] Exit to start menu");
                            inputOne = CheckAndReturnCustomerOptionChosen(Console.ReadLine(), 4);

                            if (inputOne == "1")
                            {
                                whileInSecondaryMenu = false;
                                menuSwitch = 9;
                            }
                            else if (inputOne == "2")
                            {
                                Console.WriteLine("\n------------------------------------------");
                                Console.WriteLine("Name: " + retrievedCustomer.firstName + " " + retrievedCustomer.lastName);
                                Console.WriteLine("ID: " + retrievedCustomer.customerID);
                                Console.WriteLine("\nAddress: \n" + retrievedCustomer.customerAddress.street + "\n"+ retrievedCustomer.customerAddress.city + "\n" + retrievedCustomer.customerAddress.state + "\n" + retrievedCustomer.customerAddress.zip);
                                Console.WriteLine("------------------------------------------\n");
                            }
                            else if (inputOne == "3")
                            {
                                orderList = DBRHandler.GetListOfOrdersByCustomerID(retrievedCustomer.customerID, context);

                                foreach(BusinessLogic.Objects.Order BLOrder in orderList)
                                {
                                    BLOrder.customerProductList = DBRHandler.GetListOrderProductByOrderID(BLOrder, context);
                                    BLOrder.storeLocation = DBRHandler.GetStoreInformationFromOrderNumber(BLOrder.orderID, context);
                                }

                                if (orderList == null || orderList.Count == 0)
                                {
                                    Console.WriteLine("No orders to display under " + retrievedCustomer.firstName + " " + retrievedCustomer.lastName);
                                }
                                else
                                {
                                    foreach (BusinessLogic.Objects.Order order in orderList)
                                    {
                                        Console.WriteLine("-------------------------------");
                                        Console.WriteLine("Store Number: " + order.storeLocation.storeNumber);
                                        Console.WriteLine("Order Number: " + order.orderID);
                                        foreach (BusinessLogic.Objects.Product product in order.customerProductList)
                                        {
                                            Console.WriteLine(product.name + ": " + product.amount);
                                        }
                                    }
                                    Console.WriteLine("-------------------------------\n");
                                }
                            }
                            else if (inputOne == "4")
                            {
                                whileInSecondaryMenu = false;
                                retrievedCustomer = new BusinessLogic.Objects.Customer(); //resets the customer data that was retrieved by this point in the menu
                                menuSwitch = 1;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, please type one of the following options");
                            }
                        }
                        whileInSecondaryMenu = true; //resets menu true to go into next menu
                        break;
                    case 7: //manager store view menu
                        while (whileInSecondaryMenu)
                        {
                            Console.WriteLine("Options\n[1] View Store Information\n[2] View Store Inventory\n[3] View all customer order history\n[4] Exit to start menu");
                            inputOne = CheckAndReturnCustomerOptionChosen(Console.ReadLine(), 4);

                            if (inputOne == "1") //View store info
                            {
                                Console.WriteLine("------------ Information for Store Number " + retrievedStore.storeNumber + " ------------");
                                Console.WriteLine("Address: \nStreet: " + retrievedStore.address.street + "\nCity: " + retrievedStore.address.city + "\nState: " + retrievedStore.address.state
                                    + "\nZip: " + retrievedStore.address.zip + "\n");
                            }
                            else if (inputOne == "2") //View store inventory
                            {
                                retrievedStore.storeInventory = DBRHandler.GetStoreInventoryByStoreNumber(retrievedStore.storeNumber, context);
                                Console.WriteLine("------------ Store inventory ------------");

                                foreach (BusinessLogic.Objects.Product BLProd in retrievedStore.storeInventory.productData)
                                {
                                    Console.WriteLine(BLProd.name + ": " + BLProd.amount);
                                }
                                Console.WriteLine("\n");
                            }
                            else if (inputOne == "3")
                            {
                                orderList = new List<BusinessLogic.Objects.Order>();

                                orderList = DBRHandler.GetListOfOrdersFromStoreNumber(retrievedStore.storeNumber, context);
                                foreach (BusinessLogic.Objects.Order BLOrder in orderList)
                                {
                                    BLOrder.customerProductList = DBRHandler.GetListOrderProductByOrderID(BLOrder, context);
                                    BLOrder.storeLocation = DBRHandler.GetStoreInformationFromOrderNumber(BLOrder.orderID, context);
                                }

                                if (orderList == null || orderList.Count == 0)
                                {
                                    Console.WriteLine("No orders to display under " + retrievedCustomer.firstName + " " + retrievedCustomer.lastName);
                                }
                                else
                                {
                                    foreach (BusinessLogic.Objects.Order order in orderList)
                                    {
                                        if (order.storeLocation.storeNumber == retrievedStore.storeNumber)
                                        {
                                            Console.WriteLine("-------------------------------");
                                            Console.WriteLine("Store Number: " + order.storeLocation.storeNumber);
                                            Console.WriteLine("Order Number: " + order.orderID);
                                            Console.WriteLine("Customer ID: " + order.customer.customerID);
                                            foreach (BusinessLogic.Objects.Product product in order.customerProductList)
                                            {
                                                Console.WriteLine(product.name + ": " + product.amount);
                                            }
                                        }
                                    }
                                    Console.WriteLine("-------------------------------\n");

                                }
                            }
                            else if (inputOne == "4")
                            {
                                retrievedManager = new BusinessLogic.Objects.Manager();
                                retrievedStore = new BusinessLogic.Objects.Store();

                                menuSwitch = 1;
                                whileInSecondaryMenu = false;
                            }
                        }
                        whileInSecondaryMenu = true;
                        break;
                    case 8: //Order menu
                        bool decided = false;
                        string temp;
                        while (whileInSecondaryMenu)
                        {
                            while (decided == false)
                            {
                                try
                                {
                                    inputOrder = new BusinessLogic.Objects.Order();
                                    BusinessLogic.Objects.Product inputProd = new BusinessLogic.Objects.Product(); 

                                    retrievedStore.storeInventory = DBRHandler.GetStoreInventoryByStoreNumber(retrievedStore.storeNumber, context);

                                    foreach (BusinessLogic.Objects.Product prod in retrievedStore.storeInventory.productData)
                                    {
                                        inputProd = prod;
                                        inputProd.amount = 0;
                                        Console.WriteLine("How many " + prod.name + " would you like to order?\n");
                                        temp = Console.ReadLine();
                                        inputProd.amount = Int32.Parse(temp);

                                        inputOrder.customerProductList.Add(inputProd);                          
                                    }
                                    Console.WriteLine("---------------------------");
                                    Console.WriteLine("Your order consists of: \n");
                                    foreach (BusinessLogic.Objects.Product prod in inputOrder.customerProductList)
                                    {

                                        Console.WriteLine(prod.name + ": " + prod.amount + "\n");
                                    }

                                    Console.WriteLine("Is this alright?" + "\n[1] Yes\n[2] No");
                                    inputOne = CheckAndReturnCustomerOptionChosen(Console.ReadLine(), 2);

                                    if (inputOne == "1")
                                    {
                                        decided = true;
                                        Console.WriteLine("Please wait while your order is created. . .\n");

                                        //uses input handler to input order into DB

                                        inputOrder.customer = retrievedCustomer;
                                        inputOrder.storeLocation = retrievedStore;

                                        bool goodOrder = inputOrder.CheckOrderIsValid();
                                        //goodOrder = retrievedStore.storeInventory.CheckOrderAgainstInventory(inputOrder.customerProductList);

                                        if (goodOrder == true)
                                        {
                                            try
                                            {
                                                InputWholeOrderAndUpdateInventory(inputOrder, retrievedStore ,context);

                                                menuSwitch = 6;
                                                whileInSecondaryMenu = false;
                                                Console.WriteLine("Order successfully created! Thank you for your business!\nReturning back to customer menu. . . \n");

                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Unable to perform the operation: \n");
                                                storeLogger.Info(e);                               
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Order was invalid! Please try again with acceptable values!");
                                            inputOrder = new Order();
                                            decided = false;
                                        }
                                    }
                                    else if (inputOne == "2")
                                    {
                                        Console.WriteLine("Please type in your order once more with the desired values.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input, please type one of the following options.");
                                    }

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message + "\nPlease enter correct numerical values for your order.");
                                }   
                            }

                        }
                        whileInSecondaryMenu = true;
                        break;
                    case 9: //Select order store menu
                        while (whileInSecondaryMenu)
                        {
                            Console.WriteLine("What store number are you ordering from?");
                            inputOne = Console.ReadLine();
                            isNumeric = int.TryParse(inputOne, out n);
                            if (isNumeric == false)
                            {
                                Console.WriteLine("Invalid characters. Please try again with a numerical value");
                                break;
                            }
                            else //if the input only has numbers in it
                            {
                                storeNum = Int32.Parse(inputOne);

                                try
                                {
                                    retrievedStore = DBRHandler.GetStoreFromStoreNumber(storeNum, context);
                                    menuSwitch = 8;
                                    whileInSecondaryMenu = false;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error finding store with input ID: " + e.Message + "\n");
                                }
                            }
                        }
                        whileInSecondaryMenu = true;
                        break;
                    default:
                            Console.WriteLine("Default case");
                            break;
                }
            }
        }

        public static void InputWholeOrderAndUpdateInventory(Order inputOrder, BusinessLogic.Objects.Store inputStore, StoreApplicationContext context)
        {
            DBIHandler.InputOrder(inputOrder, context);
            DBIHandler.InputOrderProduct(inputOrder, context.Orders.Count(), context);
        }

        //Used for checking menue options are not null or invalid given the input, and the max number of options on a given menue
        public static string CheckAndReturnCustomerOptionChosen(string input, int maxNum)
        {
            bool correctInput = false;
            int inputInt;
            int n;
            bool isNumeric = false;

            while(correctInput == false)
            {
                if (input == null)
                {
                    Console.WriteLine("Invalid input. Insert correct input option\n");
                    return null;
                }
                else
                {
                    foreach (char thing in input)
                    {
                        isNumeric = int.TryParse(thing.ToString(), out n);
                        if (isNumeric == false)
                        {
                            Console.WriteLine("Invalid input. Only insert a number option.\n");
                            return null;        
                        }
                        else
                        {
                            isNumeric = true;
                        }
                    }
                    if (isNumeric == false)
                    {
                        Console.WriteLine("Invalid input. Only insert an umber option.\n");
                    }
                    else
                    {
                        inputInt = Int32.Parse(input);
                        if (inputInt > maxNum)
                        {
                            Console.WriteLine("Invalid input. Insert correct number from the list below\n");
                            return null;
                        }
                        else
                        {
                            return input;
                        }
                    }
                }
            }

            return input;
        }
    }
}
