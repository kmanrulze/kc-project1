using Microsoft.EntityFrameworkCore;
using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.Entities;
using StoreApp.DataLibrary.Handlers;
using System;
using System.Collections.Generic;
using Xunit;

namespace StoreApp.Tests
{
    public class UnitTests
    {
        private readonly TestVarGeneration testVariable = new TestVarGeneration();
        public static RetrieveDatabaseHandler DBRHandler = new RetrieveDatabaseHandler();
        public static InputDatabaseHandler DBIHandler = new InputDatabaseHandler();

        internal static string connectionString = DBRHandler.GetConnectionString();
        DbContextOptions<StoreApplicationContext> options = new DbContextOptionsBuilder<StoreApplicationContext>()
            .UseSqlServer(connectionString)
            .Options;

        /// <summary>
        /// Test to determine if the customer data is retrieved from a proper ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CheckGetCustomerReturnsProperValues(int testID)
        {
            
            using var context = new StoreApplicationContext(options);
            BusinessLogic.Objects.Customer testCustomer = new BusinessLogic.Objects.Customer();

            testCustomer = DBRHandler.GetCustomerDataFromID(testID, context);
            Assert.True(testCustomer.CheckCustomerNotNull());


        }

        /// <summary>
        /// Test to determine if the data is retrieved for a manager with a proper ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetManagerReturnsProperValues(int testID)
        {
            using var context = new StoreApplicationContext(options);
            BusinessLogic.Objects.Manager testManager = new BusinessLogic.Objects.Manager();

            testManager = DBRHandler.GetManagerDataFromID(testID, context);
            Assert.True(testManager.CheckIfManagerNull());
        }

        /// <summary>
        /// Test to determine if the data is retrieved for a store with a proper Store ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CheckStoreDataReturnProperValues(int testID)
        {

            using var context = new StoreApplicationContext(options);
            BusinessLogic.Objects.Store testStore = new BusinessLogic.Objects.Store();

            testStore = DBRHandler.GetStoreFromStoreNumber(testID, context);
            Assert.True(testStore.CheckStoreNotNull());
            
        }

        /// <summary>
        /// Test to determine if the product data for a store to a store object is retrieved with a proper Store ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CheckStoreProductReturnsProperValues(int testID)
        {
            using var context = new StoreApplicationContext(options);
            BusinessLogic.Objects.Store testStore = new BusinessLogic.Objects.Store();

            testStore = DBRHandler.GetStoreFromStoreNumber(testID, context);
            testStore.storeInventory = DBRHandler.GetStoreInventoryByStoreNumber(testID, context);
            Assert.True(testStore.storeInventory.CheckIfProductListNotNull());
        }

        /// <summary>
        /// Tests to determine if tests return false in the event that a store inventory has no products within it
        /// </summary>
        [Fact]
        public void CheckStoreProductReturnsFalseIfNoProduct()
        {
            using var context = new StoreApplicationContext(options);
            BusinessLogic.Objects.Store testStore = new BusinessLogic.Objects.Store();

            Assert.False(testStore.storeInventory.CheckIfProductListNotNull());
        }

        /// <summary>
        /// Test to determine if the handler retrieves a proper list of order ID's given a customer ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CheckIfOrderDataReturnsProperIDs(int testID)
        {
            using var context = new StoreApplicationContext(options);
            List<BusinessLogic.Objects.Order> testListOrder = new List<Order>();

            testListOrder = DBRHandler.GetListOfOrdersByCustomerID(testID, context);

            foreach (BusinessLogic.Objects.Order testOrder in testListOrder)
            {
                if (testOrder.customer.customerID == testID)
                {
                    Assert.True(testOrder.CheckOrderHasIDs());
                }
            }
        }
    }
}
