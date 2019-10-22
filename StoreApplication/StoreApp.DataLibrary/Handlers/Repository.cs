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

        public async Task<List<BusinessLogic.Objects.Product>> GetListStockedProductsForStoreAsync(int StoreID)
        {
            List<BusinessLogic.Objects.Product> BLProdStockList = new List<BusinessLogic.Objects.Product>();
            List<InventoryProduct> CTXInventory = await _context.InventoryProduct.AsNoTracking().Where(ip => ip.StoreNumber == StoreID).ToListAsync();

            foreach(Entities.Product CTXProd in _context.Product)
            {
                foreach(Entities.InventoryProduct CTXInvProd in CTXInventory)
                {
                    if (CTXInvProd.InventoryProductId == CTXProd.ProductTypeId)
                    {
                        BLProdStockList.Add(ParseHandler.ContextProductInformationToLogicProduct(CTXProd));
                    }
                }
            }
            return BLProdStockList;
        }

        public async Task AddPlacedOrderToCustomerAsync(int customerID, BusinessLogic.Objects.Order BLOrd)
        {
            try
            {
                _context.Orders.Add(ParseHandler.LogicOrderToContextOrder(BLOrd));
                _context.SaveChanges();
                Order BLNewOrder = ParseHandler.ContextOrderToLogicOrder(_context.Orders.OrderByDescending(s => s.OrderId).First(o => o.CustomerId == customerID));
                int newOrderID = BLNewOrder.orderID;
                //BLNewOrder.storeLocation = await GetStoreInformation(BLNewOrder.storeLocation.storeNumber);

                foreach (BusinessLogic.Objects.Product BLProd in BLOrd.customerProductList)
                {
                    _context.OrderProduct.Add(ParseHandler.LogicProductToContextOrderProduct(newOrderID, BLProd));
                }
                //In the future, put the update inventory function before the savechanges for inventory validation purposes
                _context.SaveChanges();
                //await UpdateInventoryFromPlacedOrder(BLNewOrder);
                
            }
            catch
            {
                throw new Exception("Unable to commit the order to the database");
            }
        }

        public async Task UpdateInventoryFromPlacedOrder(Order BLOrd)
        {
            try
            {
                var CTXOrdProdList = await _context.OrderProduct.Where(o => o.OrderId == BLOrd.orderID).ToListAsync();

#warning Currently nonfunctional when called by order placer. Throws exception due to the line below for some reason. Fix later. 
                List<InventoryProduct> CTXInvProdList = await _context.InventoryProduct.ToListAsync();

                foreach (InventoryProduct CTXIP in CTXInvProdList)
                {
                    if (CTXIP.StoreNumber == BLOrd.storeLocation.storeNumber)
                    {
                        CTXInvProdList.Add(CTXIP);
                    }
                }
                //BusinessLogic.Objects.Order BLOrder = ParseHandler.ContextOrderToLogicOrder(CTXOrder);

                if (CTXOrdProdList != null)
                {
                    foreach (Entities.OrderProduct CTXOrdProd in CTXOrdProdList)
                    {
                        foreach (Entities.InventoryProduct CTXInvProd in CTXInvProdList)
                        {
                            if (CTXOrdProd.OrderProductId == CTXInvProd.InventoryProductId)
                            {
                                if (CTXInvProd.ProductAmount - CTXOrdProd.ProductAmount == 0)
                                {
                                    _context.InventoryProduct.Remove(CTXInvProd);
                                }
                                else
                                {
                                    CTXInvProd.ProductAmount = CTXInvProd.ProductAmount - CTXOrdProd.ProductAmount;
                                }
                            }
                        }
                    }
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw new Exception("Unable to update inventory from order input");
            }


        }
    }
}
