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
    }
}
