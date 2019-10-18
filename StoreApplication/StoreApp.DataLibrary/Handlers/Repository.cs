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

                foreach (Entities.Orders CTXOrder in _context.Orders)
                {
                    BLListOrders.Add(ParseHandler.ContextOrderToLogicOrder(CTXOrder));
                }
                foreach (BusinessLogic.Objects.Order BLOrder in BLListOrders)
                {
                    foreach (Entities.OrderProduct CTXOrdProd in _context.OrderProduct.AsNoTracking().Where(op => op.StoreNumber == storeID).ToHashSet())
                    {
                        if (BLOrder.orderID == CTXOrdProd.OrderId)
                        {
                            BLOrder.customerProductList.Add(ParseHandler.ContextOrderProductToLogicProduct(CTXOrdProd));
                        }
                    }
                }
                return BLListOrders;
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for store number: " + storeID);
            }
        }
    }
}
