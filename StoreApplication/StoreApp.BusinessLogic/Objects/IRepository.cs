using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.BusinessLogic.Objects
{
    public interface IRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task AddCustomerAsync();
    }
}
