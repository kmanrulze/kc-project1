using System;
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
            return View(viewModel);
        }
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manager/Details/5
        public async Task<ActionResult> DetailsAsync(int ManagerID)
        {
            try
            {
                Manager retrievedManager = await _repository.GetManagerInformation(ManagerID);
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


                if (!ModelState.IsValid)
                {
                    return View(nameof(Login));
                }

                return View(viewModel);
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
        public async Task<ActionResult> OrderInformationByStore(int storeID)
        {
            
            List<BusinessLogic.Objects.Order> storeOrderList = await _repository.GetListAllOrdersForStore(storeID);

            if(ViewBag.LoggedManager == 0)
            {
                return RedirectToAction(nameof(InvalidManager));
            }
            else
            {
                var viewModel = storeOrderList.Select(o => new OrderViewModel
                {
                    OrderID = o.orderID,
                    CustomerID = o.customer.customerID,
                    StoreNumber = o.storeLocation.storeNumber,
                    Products = o.customerProductList.Select(p => p.name + ": " + p.amount).ToList(),
                    CustomerName = o.customer.firstName + " " + o.customer.lastName

                });
                return View(viewModel);
            }

        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manager/Edit/5
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

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
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