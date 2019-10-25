﻿using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.Entities;

namespace StoreApp.DataLibrary.Handlers
{
    public static class ParseHandler
    {
        /// <summary>
        /// Converts a Entity/Context Customer into a Business Logic Customer
        /// </summary>
        /// <param name="CTXCustomer"></param>
        /// <returns></returns>
        public static StoreApp.BusinessLogic.Objects.Customer ContextCustomerToLogicCustomer(StoreApp.DataLibrary.Entities.Customer CTXCustomer)
        {
            StoreApp.BusinessLogic.Objects.Customer BLCustomer = new BusinessLogic.Objects.Customer();

            BLCustomer.CustomerAddress.street = CTXCustomer.Street;
            BLCustomer.CustomerAddress.city = CTXCustomer.City;
            BLCustomer.CustomerAddress.state = CTXCustomer.State;
            BLCustomer.CustomerAddress.zip = CTXCustomer.Zip;

            BLCustomer.customerID = CTXCustomer.CustomerId;
            BLCustomer.firstName = CTXCustomer.FirstName;
            BLCustomer.lastName = CTXCustomer.LastName;
            

            return BLCustomer;
        }
        /// <summary>
        /// Fills a context version of an OrderProduct context entry with what it can from a Business Logic Product input and an orderID
        /// </summary>
        /// <param name="BLOrder"></param>
        /// <param name="orderID"></param>
        /// <param name="BLProd"></param>
        /// <returns></returns>
        public static OrderProduct LogicProductToContextOrderProduct(int orderID, BusinessLogic.Objects.Product BLProd)
        {
            Entities.OrderProduct CTXOrdProd = new OrderProduct();

            CTXOrdProd.ProductTypeId = BLProd.productTypeID;
            CTXOrdProd.ProductAmount = BLProd.Amount;
            CTXOrdProd.OrderId = orderID;

            return CTXOrdProd;
        }
        /// <summary>
        /// returns a Manager entity into the business logic version of it
        /// </summary>
        /// <param name="CTXman"></param>
        /// <returns></returns>
        public static BusinessLogic.Objects.Manager ContextManagerToLogicManager(Entities.Manager CTXman)
        {
            StoreApp.BusinessLogic.Objects.Manager BLMan = new BusinessLogic.Objects.Manager();

            BLMan.managerID = CTXman.ManagerId;
            BLMan.firstName = CTXman.FirstName;
            BLMan.lastName = CTXman.LastName;
            BLMan.storeNumberManaged = CTXman.StoreNumber;


            return BLMan;
        }

        /// <summary>
        /// returns a Customer entity from the data given by a Business Logic version of the customer
        /// </summary>
        /// <param name="BLCustomer"></param>
        /// <returns></returns>
        public static Entities.Customer LogicCustomerToContextCustomer(BusinessLogic.Objects.Customer BLCustomer)
        {
            Entities.Customer CTXCustomer= new Entities.Customer();

            CTXCustomer.FirstName = BLCustomer.firstName;
            CTXCustomer.LastName = BLCustomer.lastName;
            CTXCustomer.Street = BLCustomer.CustomerAddress.street;
            CTXCustomer.City = BLCustomer.CustomerAddress.city;
            CTXCustomer.State = BLCustomer.CustomerAddress.state;
            CTXCustomer.Zip = BLCustomer.CustomerAddress.zip;

            return CTXCustomer;
        }
        /// <summary>
        /// Returns a business logic store from information gotten from a Store entity
        /// </summary>
        /// <param name="CTXStore"></param>
        /// <returns></returns>
        public static BusinessLogic.Objects.Store ContextStoreToLogicStore(Entities.Store CTXStore)
        {
            StoreApp.BusinessLogic.Objects.Store BLStore = new BusinessLogic.Objects.Store();

            BLStore.Address.street = CTXStore.Street;
            BLStore.Address.city = CTXStore.City;
            BLStore.Address.state = CTXStore.State;
            BLStore.Address.zip = CTXStore.Zip;

            BLStore.storeNumber = CTXStore.StoreNumber;

            return BLStore;
        }
        /// <summary>
        /// Returns an Order entity from data given by a Business Logic order
        /// </summary>
        /// <param name="BLorder"></param>
        /// <returns></returns>
        public static Entities.Orders LogicOrderToContextOrder(StoreApp.BusinessLogic.Objects.Order BLorder)
        {
            Orders CTXOrder = new Orders();

            CTXOrder.CustomerId = BLorder.Customer.customerID;
            CTXOrder.StoreNumber = BLorder.StoreLocation.storeNumber;

            return CTXOrder;
        }

        /// <summary>
        /// Returns a business logic version of an order from information given by an Order entity
        /// </summary>
        /// <param name="CTXOrder"></param>
        /// <returns></returns>
        public static Order ContextOrderToLogicOrder(Entities.Orders CTXOrder)
        {
            BusinessLogic.Objects.Order BLOrder = new Order();
            BLOrder.Customer.customerID = CTXOrder.CustomerId;
            BLOrder.orderID = CTXOrder.OrderId;
            BLOrder.StoreLocation.storeNumber = CTXOrder.StoreNumber;

            return BLOrder;
        }

        /// <summary>
        /// Returns a business logic product from information given by a Product entity
        /// </summary>
        /// <param name="CTXProd"></param>
        /// <returns></returns>
        public static BusinessLogic.Objects.Product ContextInventoryProductToLogicProduct(InventoryProduct CTXProd)
        {
            BusinessLogic.Objects.Product BLProd = new BusinessLogic.Objects.Product();

            BLProd.productTypeID = CTXProd.ProductTypeId;
            BLProd.Amount = CTXProd.ProductAmount;

            return BLProd;
        }

        /// <summary>
        /// Returns a business logic product from information given by an OrderProduct entity
        /// </summary>
        /// <param name="CTXProduct"></param>
        /// <returns></returns>
        public static BusinessLogic.Objects.Product ContextOrderProductToLogicProduct(OrderProduct CTXProduct)
        {
            BusinessLogic.Objects.Product BLProduct = new BusinessLogic.Objects.Product();

            BLProduct.productTypeID = CTXProduct.ProductTypeId;
            BLProduct.Amount = CTXProduct.ProductAmount;

            return BLProduct;
        }
        internal static BusinessLogic.Objects.Product ContextProductInformationToLogicProduct(Entities.Product CTXProd)
        {
            BusinessLogic.Objects.Product BLProd = new BusinessLogic.Objects.Product();

            BLProd.productTypeID = CTXProd.ProductTypeId;
            BLProd.name = CTXProd.ProductName;
            BLProd.price = CTXProd.ProductPrice;

            return BLProd;
        }
    }
}
