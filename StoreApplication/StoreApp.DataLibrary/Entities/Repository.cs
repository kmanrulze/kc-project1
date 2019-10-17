using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreApp.BusinessLogic;
using StoreApp.BusinessLogic.Objects;

namespace StoreApp.DataLibrary.Entities
{
    public class Repository : IRepository
    {
        private readonly StoreApplicationContext _context;
        public Repository(StoreApplicationContext context) => _context = context;

        public Task AddCustomerAsync()
        {
            throw new NotImplementedException();
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
            }) ;
        }
    }
}
