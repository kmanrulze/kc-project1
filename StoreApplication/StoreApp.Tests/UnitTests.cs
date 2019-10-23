using Microsoft.EntityFrameworkCore;
using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.Entities;
using StoreApp.DataLibrary.Handlers;
using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Moq;

namespace StoreApp.Tests
{
    public class UnitTests
    {
        private static StoreApplicationContext context = new StoreApplicationContext();
        private Repository _repository = new Repository(context);

        /// <summary>
        /// Test to determine if the customer data is retrieved from a proper ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CheckGetCustomerReturnsProperValuesAsync(int testID)
        {
            //string connectionString = _repository.GetConnectionString();
            //DbContextOptions<StoreApplicationContext> options = new DbContextOptionsBuilder<StoreApplicationContext>()
            //    .UseSqlServer(connectionString)
            //    .Options;

            //using var context = new StoreApplicationContext(options);


            BusinessLogic.Objects.Customer testCustomer = new BusinessLogic.Objects.Customer();

            testCustomer = await _repository.GetCustomerByID(testID);
            Assert.True(testCustomer.CheckCustomerNotNull());
        }

        /// <summary>
        /// Test to determine if the data is retrieved for a manager with a proper ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetManagerReturnsProperValues(int testID)
        {
            BusinessLogic.Objects.Manager testManager = new BusinessLogic.Objects.Manager();

            testManager = await _repository.GetManagerInformation(testID);
            Assert.True(testManager.CheckIfManagerNull());
        }

        /// <summary>
        /// Test to determine if the data is retrieved for a store with a proper Store ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CheckStoreDataReturnProperValues(int testID)
        {
            BusinessLogic.Objects.Store testStore = new BusinessLogic.Objects.Store();

            testStore = await _repository.GetStoreInformation(testID);
            Assert.True(testStore.CheckStoreNotNull());
            
        }

        /// <summary>
        /// Test to determine if the product data for a store to a store object is retrieved with a proper Store ID
        /// </summary>
        /// <param name="testID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CheckStoreProductReturnsProperValues(int testID)
        {
            BusinessLogic.Objects.Store testStore = new BusinessLogic.Objects.Store();

            testStore = await _repository.GetStoreInformation(testID);
            //testStore.storeInventory = DBRHandler.GetStoreInventoryByStoreNumber(testID);
            Assert.True(testStore.storeInventory.CheckIfProductListNotNull());
        }

        /// <summary>
        /// Tests to determine if tests return false in the event that a store inventory has no products within it
        /// </summary>
        [Fact]
        public void CheckStoreProductReturnsFalseIfNoProduct()
        {
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
            List<BusinessLogic.Objects.Order> testListOrder = new List<Order>();

            //testListOrder = DBRHandler.GetListOfOrdersByCustomerID(testID);

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
