﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using StoreApp.BusinessLogic.Objects;
using StoreApp.WebApp.Models;
using System.Web;

namespace StoreApp.WebApp.Controllers
{
    public class ManagerController : Controller
    {


        private readonly IRepository _repository;
        public ManagerController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Login()
        {
            var viewModel = new ManagerViewModel();
            TempData.Remove("LoggedStore");
            TempData.Remove("LoggedManager");
            return View(viewModel);
        }

        // GET: Manager/Details/
        public async Task<ActionResult> Details(int ManagerID)
        {
            bool hasValue;
            //Makes sure tempdata is clear
            try
            {
                if (TempData["LoggedManager"] == null && ManagerID == 0)
                {
                    hasValue = false;  
                }
                else
                {
                    hasValue = true;
                }
                if (hasValue == false)
                {
                    if (ManagerID == 0)
                    {
                        return RedirectToAction(nameof(InvalidManager));
                    }
                    else if (TempData["LoggedManager"] == null)
                    {
                        throw new Exception("No temp data for the store. Please ensure the temp data was kept between controllers");
                    }
                    else
                    {
                        throw new Exception("Something else went wrong with validating data in DetailsAsync in the ManagerController class");
                    }
                }
                else
                {
                    if (ManagerID == 0)
                    {
                        ManagerID = int.Parse(TempData["LoggedManager"].ToString());
                    }
                    else
                    {
                        TempData["LoggedManager"] = ManagerID;
                    }
                    
                    Manager retrievedManager = await _repository.GetManagerInformation(int.Parse(TempData["LoggedManager"].ToString()));
                    Store retrievedStore = await _repository.GetStoreInformation(retrievedManager.storeNumberManaged);

                    var viewModel = new ManagerViewModel
                    {
                        FirstName = retrievedManager.firstName,
                        LastName = retrievedManager.lastName,
                        StoreID = retrievedStore.storeNumber,
                        ManagerID = retrievedManager.managerID,
                        StoreStreet = retrievedStore.address.street,
                        StoreCity = retrievedStore.address.city,
                        StoreState = retrievedStore.address.state,
                        StoreZip = retrievedStore.address.zip
                    };
                    TempData["LoggedStore"] = retrievedStore.storeNumber;

                    TempData.Keep();

                    if (!ModelState.IsValid)
                    {
                        return View(nameof(Login));
                    }

                    return View(viewModel);
                }
            }
            catch (InvalidOperationException)
            {

                return RedirectToAction(nameof(InvalidManager));
            }

        }
        public ActionResult InvalidManager(int inputManagerID)
        {
            return View(inputManagerID);
        }
        public async Task<ActionResult> OrderInformationByStore()
        {
            bool hasValue;
            if (TempData["LoggedManager"] == null || TempData["LoggedStore"] == null)
            {
                hasValue = false;
            }
            else
            {
                hasValue = true;
            }
            if (hasValue == false)
            {
                throw new Exception("No temp data for the store. Please ensure the temp data was kept between controllers");
            }
            else
            {
                int ManagerID = int.Parse(TempData["LoggedManager"].ToString());
                int StoreID = int.Parse(TempData["LoggedStore"].ToString());
                List<BusinessLogic.Objects.Order> storeOrderList = await _repository.GetListAllOrdersForStore(StoreID);

                var viewModel = storeOrderList.Select(o => new OrderViewModel
                {
                    OrderID = o.orderID,
                    CustomerID = o.customer.customerID,
                    StoreNumber = o.storeLocation.storeNumber,
                    Products = o.customerProductList.Select(p => p.name + ": " + p.amount).ToList(),
                    CustomerName = o.customer.firstName + " " + o.customer.lastName

                });

                TempData.Keep("LoggedManager");
                TempData.Keep("LoggedStore");
                return View(viewModel);
            }

        }
    }
}