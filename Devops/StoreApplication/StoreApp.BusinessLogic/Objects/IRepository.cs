using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.BusinessLogic.Objects
{
    public interface IRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task AddCustomerAsync(Customer BLCustomer);
        Task<Manager> GetManagerInformation(int ManagerID);
        Task<Store> GetStoreInformation(int StoreID);
        Task<List<Order>> GetListAllOrdersForStore(int storeID);
        Task<List<BusinessLogic.Objects.Product>> GetOrderProductListByID(int orderID);
        Task<BusinessLogic.Objects.Customer> GetCustomerByID(int CustomerID);
        Task<List<BusinessLogic.Objects.Order>> GetListAllOrdersFromCustomer(int customerID);
        public Customer GetLastCustomerWithFirstLast(string firstName, string lastName);
        Task<List<BusinessLogic.Objects.Product>> GetListStockedProductsForStoreAsync(int StoreID);
        Task AddPlacedOrderToCustomerAsync(int customerID, BusinessLogic.Objects.Order BLOrd);
        Task UpdateInventoryFromPlacedOrder(Order BLOrd);
    }
}
