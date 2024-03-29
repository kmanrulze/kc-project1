﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            TempData.Remove("LoggedCustomer");
            return View(viewModel);
        }

        // GET: Customer/Profile : Displays stuff from the given ID
        public async Task<ActionResult> Profile(int CustomerID)
        {
            bool hasValue;
            try
            {
                if (TempData["LoggedCustomer"] == null && CustomerID == 0)
                {
                    hasValue = false;
                }
                else
                {
                    hasValue = true;
                }
                if (hasValue == false)
                {
                    if (CustomerID == 0)
                    {
                        return RedirectToAction(nameof(InvalidCustomer));
                    }
                    else if (TempData["LoggedManager"] == null)
                    {
                        throw new Exception("No temp data for the customer. Please ensure the temp data was kept between controllers");
                    }
                    else
                    {
                        throw new Exception("Something else went wrong with validating data in ProfileAsync in the CustomerController class");
                    }
                }
                else
                {
                    if (CustomerID == 0)
                    {
                        CustomerID = int.Parse(TempData["LoggedCustomer"].ToString());
                    }
                    else
                    {
                        TempData["LoggedCustomer"] = CustomerID;
                    }

                    BusinessLogic.Objects.Customer customer = await _repository.GetCustomerByID(int.Parse(TempData["LoggedCustomer"].ToString()));
                    List<BusinessLogic.Objects.Order> orders = await _repository.GetListAllOrdersFromCustomer(customer.customerID);

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
                        CustomerID = customer.customerID,
                        FirstName = customer.firstName,
                        LastName = customer.lastName,
                        Street = customer.CustomerAddress.street,
                        City = customer.CustomerAddress.city,
                        State = customer.CustomerAddress.state,
                        Zip = customer.CustomerAddress.zip,
                        CustomerOrderIDs = orders.Select(oID => oID.orderID).ToList(),
                        CustomerProduct = orders.SelectMany(op => op.CustomerProductList).ToList(),
                        OrderStore = orders.Select(os => os.StoreLocation.storeNumber).ToList(),
                        CustomerOrders = orders

                    };
                    TempData.Keep("LoggedCustomer");

                    return View(viewModel);
                }
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

        public ActionResult Create()
        {
            CreateCustomerViewModel viewModel = new CreateCustomerViewModel();
            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCustomerViewModel VMCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(VMCustomer);
                }

                var customer = new BusinessLogic.Objects.Customer
                {
                    firstName = VMCustomer.FirstName,
                    lastName = VMCustomer.LastName,
                    CustomerAddress = new Address
                    {
                        street = VMCustomer.Street,
                        city = VMCustomer.City,
                        state = VMCustomer.State,
                        zip = VMCustomer.Zip
                    }
                };

                await _repository.AddCustomerAsync(customer);
                try
                {
                    BusinessLogic.Objects.Customer newCustomer = _repository.GetLastCustomerWithFirstLast(VMCustomer.FirstName, VMCustomer.LastName);
                    TempData["LoggedCustomer"] = newCustomer.customerID;
                    return RedirectToAction(nameof(Profile));
                }
                catch (InvalidOperationException e)
                {
                    ModelState.AddModelError("Failed to get the new added customer's ID", e.Message);
                    return View(VMCustomer);
                }  
            }
            catch
            {
                return View(VMCustomer);
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