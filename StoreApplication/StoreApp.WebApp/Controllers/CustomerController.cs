﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.DataLibrary.Entities;
using StoreApp.BusinessLogic.Objects;
using StoreApp.WebApp.Models;

namespace StoreApp.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;
        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: Customer/Login : Logs in by ID
        public ActionResult Login()
        {
            var viewModel = new CustomerProfileViewModel();
            return View(viewModel);
        }

        // GET: Customer/Profile : Displays stuff from the given ID
        public async Task<ActionResult> ProfileAsync(int CustomerID)
        {
            try
            {
                CustomerID = 1;
                BusinessLogic.Objects.Customer customer = await _repository.GetCustomerByID(CustomerID);
                List<BusinessLogic.Objects.Order> orders = await _repository.GetListAllOrdersFromCustomer(CustomerID);

                if (!ModelState.IsValid)
                {
                    return View(nameof(Login));
                }
                if (customer.customerID == 0)
                {
                    return RedirectToAction(nameof(InvalidCustomer));
                }

                var viewModel = new CustomerProfileViewModel
                {
                    ID = customer.customerID,
                    FirstName = customer.firstName,
                    LastName = customer.lastName,
                    Street = customer.customerAddress.street,
                    City = customer.customerAddress.city,
                    State = customer.customerAddress.state,
                    Zip = customer.customerAddress.zip,
                    CustomerOrderIDs = orders.Select(oID => oID.orderID).ToList(),
                    CustomerProduct = orders.SelectMany(op => op.customerProductList).ToList(),
                    OrderStore = orders.Select(os => os.storeLocation.storeNumber).ToList(),
                    CustomerOrders = orders
                            
                };

                return View(viewModel);
            }
            catch (InvalidOperationException)
            {

                return RedirectToAction(nameof(InvalidCustomer));
            }
        }
        public ActionResult InvalidCustomer(int inputCustomerID)
        {
            return View(inputCustomerID);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(CreateCustomerViewModel VMCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(Index));
                }

                var customer = new BusinessLogic.Objects.Customer
                {
                    firstName = VMCustomer.FirstName,
                    lastName = VMCustomer.LastName,
                    customerAddress = new Address
                    {
                        street = VMCustomer.Street,
                        city = VMCustomer.City,
                        state = VMCustomer.State,
                        zip = VMCustomer.Zip
                    }
                };

                await _repository.AddCustomerAsync(customer);
                BusinessLogic.Objects.Customer newCustomer = await _repository.GetLastCustomerWithFirstLast(VMCustomer.FirstName, VMCustomer.LastName);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}