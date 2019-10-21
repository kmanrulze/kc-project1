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
    public class OrderController : Controller
    {
        private readonly IRepository _repository;
        public OrderController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult StoreSelect()
        {
            if (TempData["LoggedCustomer"] != null)
            {
                TempData.Keep("LoggedCustomer");
                var viewModel = new StoreViewModel();

                return View(viewModel);
            }
            else
            {
                throw new Exception("Need to be logged in as customer first");
            }


        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StoreSelect(int StoreID)
        {
            try
            {

                if (TempData["LoggedCustomer"] != null)
                {
                    TempData.Keep("LoggedCustomer");
                    TempData["SelectedStore"] = StoreID;


                    //return RedirectToAction("Profile", "Customer");
                    return RedirectToAction(nameof(CreateCart));
                }
                else
                {
                    throw new Exception("Need to be logged in as customer first");
                }


            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> CreateCart()
        {
            int StoreID = int.Parse(TempData["SelectedStore"].ToString());
            CreateOrderViewModel VMOrderView = new CreateOrderViewModel();
            VMOrderView.Products = await _repository.GetListStockedProductsForStoreAsync(StoreID);
            return View(VMOrderView);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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