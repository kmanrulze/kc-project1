using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreApp.BusinessLogic;
using StoreApp.BusinessLogic.Objects;
using StoreApp.DataLibrary.Handlers;

namespace StoreApp.DataLibrary.Entities
{
    public class Repository : IRepository
    {
        private readonly StoreApplicationContext _context;

        public Repository(StoreApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BusinessLogic.Objects.Customer>> GetAllCustomersAsync()
        {
            List<Customer> entities = await _context.Customer
                    .ToListAsync();

            return entities.Select(e => new BusinessLogic.Objects.Customer
            {
                firstName = e.FirstName,
                lastName = e.LastName,
                customerID = e.CustomerId,
                customerAddress = new Address
                {
                    street = e.Street,
                    city = e.City,
                    state = e.State,
                    zip = e.Zip

                }
            }).ToHashSet() ;
        }
        public async Task AddCustomerAsync(BusinessLogic.Objects.Customer BLCustomer)
        {
            var entity = new Customer
            {
                FirstName = BLCustomer.firstName,
                LastName = BLCustomer.lastName,
                Street = BLCustomer.customerAddress.street,
                City = BLCustomer.customerAddress.city,
                State = BLCustomer.customerAddress.state,
                Zip = BLCustomer.customerAddress.zip
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BusinessLogic.Objects.Manager> GetManagerInformation(int ManagerID)
        {
            try
            {
                Manager CTXMan = await _context.Manager.AsNoTracking().FirstAsync(m => m.ManagerId == ManagerID);

                return ParseHandler.ContextManagerToLogicManager(CTXMan);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Was unable to get a manager with that ID");
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong in getting the manager's information: " + e.Message);
            }

        }

        public async Task<BusinessLogic.Objects.Store> GetStoreInformation(int StoreID)
        {
            try
            {
                Store CTXStore = await _context.Store.AsNoTracking().FirstAsync(s => s.StoreNumber == StoreID);

                return ParseHandler.ContextStoreToLogicStore(CTXStore);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Unable to get a store connected to the manager's ID");
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong in getting the store's information: " + e.Message);
            }

        }

        public async Task<List<Order>> GetListAllOrdersForStore(int storeID)
        {
            try
            {
                List<BusinessLogic.Objects.Order> BLListOrders = new List<Order>();

                foreach (Entities.Orders CTXOrder in _context.Orders.AsNoTracking().Where(o => o.StoreNumber == storeID).ToHashSet())
                {
                    BLListOrders.Add(ParseHandler.ContextOrderToLogicOrder(CTXOrder));
                }
                foreach (BusinessLogic.Objects.Order BLOrd in BLListOrders)
                {
                    BLOrd.storeLocation = await GetStoreInformation(BLOrd.storeLocation.storeNumber);
                }
                foreach (Order BLOrdToFill in BLListOrders)
                {
                    BLOrdToFill.customerProductList = await GetOrderProductListByID(BLOrdToFill.orderID);
                    BLOrdToFill.customer = await GetCustomerByID(BLOrdToFill.customer.customerID);
                }
                return BLListOrders;
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for store number: " + storeID);
            }
        }

        public async Task<BusinessLogic.Objects.Customer> GetCustomerByID(int CustomerID)
        {
            try
            {
                return ParseHandler.ContextCustomerToLogicCustomer(await _context.Customer.AsNoTracking().FirstAsync(c => c.CustomerId == CustomerID));
            }
            catch (Exception e)
            {
                throw new Exception("Failed to retrieve customer information for customer ID: " + CustomerID + "\nException thrown: " + e.Message);
            }
            
        }

        public async Task<List<BusinessLogic.Objects.Product>> GetOrderProductListByID(int orderID)
        {
            try
            {
                List<Entities.OrderProduct> CTXOrdProdList = await _context.OrderProduct.AsNoTracking().Where(op => op.OrderId == orderID).ToListAsync();
                List<BusinessLogic.Objects.Product> BLProdList = new List<BusinessLogic.Objects.Product>();

                foreach (OrderProduct CTXOrdProd in CTXOrdProdList)
                {
                    BLProdList.Add(ParseHandler.ContextOrderProductToLogicProduct(CTXOrdProd));
                }
                foreach (Entities.Product CTXProduct in _context.Product)
                {
                    foreach (BusinessLogic.Objects.Product BLProd in BLProdList)
                    {
                        if (CTXProduct.ProductTypeId == BLProd.productTypeID)
                        {
                            BLProd.name = CTXProduct.ProductName;
                        }
                    }
                }

                return BLProdList;
            }
            catch
            {
                throw new Exception("Failed to retrieve order product information for order ID: " + orderID);
            }

        }

        public async Task<List<BusinessLogic.Objects.Order>> GetListAllOrdersFromCustomer(int customerID)
        {
            try
            {
                List<BusinessLogic.Objects.Order> BLListOrders = new List<Order>();

                foreach (Entities.Orders CTXOrder in _context.Orders.AsNoTracking().Where(o => o.CustomerId == customerID).ToHashSet())
                {
                    BLListOrders.Add(ParseHandler.ContextOrderToLogicOrder(CTXOrder));
                }
                foreach (BusinessLogic.Objects.Order BLOrd in BLListOrders)
                {
                    BLOrd.storeLocation = await GetStoreInformation(BLOrd.storeLocation.storeNumber);
                }
                foreach (Order BLOrdToFill in BLListOrders)
                {
                    BLOrdToFill.customerProductList = await GetOrderProductListByID(BLOrdToFill.orderID);
                    BLOrdToFill.customer = await GetCustomerByID(BLOrdToFill.customer.customerID);
                }
                return BLListOrders;
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for customer number: " + customerID);
            }
        }

        public BusinessLogic.Objects.Customer GetLastCustomerWithFirstLast(string firstName, string lastName)
        {
            try
            {
                Customer CTXCust = new Customer();
                //Customer CTXCustomer = await _context.Customer.Where(c => c.FirstName == firstName).LastAsync();
                foreach (Customer CTXCustomer in _context.Customer.Where(c => c.FirstName == firstName && c.LastName == lastName))
                {
                    CTXCust = CTXCustomer;
                }
                return ParseHandler.ContextCustomerToLogicCustomer(CTXCust);
            }

            catch (InvalidOperationException e)
            {
                throw new Exception("Failed to get the new customer with first name: " + firstName + "\nand lastName: " + lastName + "\nException: " + e.Message);
            }
        }

        public List<BusinessLogic.Objects.Product> GetListStockedProducts()
        {
            List<BusinessLogic.Objects.Product> BLProdStockList = new List<BusinessLogic.Objects.Product>();

            foreach(Entities.Product CTXProd in _context.Product)
            {
                //BLProdStockList.Add(ParseHandler.ContextProductStockToLogicProduct(CTXProd));
            }
            return BLProdStockList;
        }
    }
}
