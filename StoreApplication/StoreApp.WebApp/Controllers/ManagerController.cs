using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.BusinessLogic.Objects;
using StoreApp.WebApp.Models;

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
                Manager manager = await _repository.GetManagerInformation(ManagerID);
                Store store = await _repository.GetStoreInformation(manager.storeNumberManaged);

                var viewModel = new ManagerViewModel
                {
                    FirstName = manager.firstName,
                    LastName = manager.lastName,
                    StoreID = store.storeNumber,
                    ManagerID = manager.managerID,
                    StoreStreet = store.address.street,
                    StoreCity = store.address.city,
                    StoreState = store.address.state,
                    StoreZip = store.address.zip
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
        [HttpGet("{StoreNumber}")]
        public async Task<ActionResult> OrderInformationByStore(int StoreNumber, int ManagerID)
        {


            List<BusinessLogic.Objects.Order> storeOrderList = await _repository.GetListAllOrdersForStore(StoreNumber);

            if(ManagerID == 0 || ManagerID == 0)
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