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

        //GET Store selected
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

        // POST: Order/Create once store is selected
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreSelect(int StoreID)
        {
            try
            {

                if (TempData["LoggedCustomer"] != null)
                {
                    TempData["SelectedStore"] = StoreID;

                    TempData.Keep("SelectedStore");
                    TempData.Keep("LoggedCustomer");


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
        // GET the store and the products it has to display for an order to be created via model binding
        public async Task<ActionResult> CreateCart()
        {
            if (TempData["SelectedStore"] != null && TempData["LoggedCustomer"] != null)
            {
                int StoreID = int.Parse(TempData["SelectedStore"].ToString());
                TempData.Keep("SelectedStore");
                TempData.Keep("LoggedCustomer");
                CreateOrderViewModel VMOrderView = new CreateOrderViewModel();
                VMOrderView.Products = new List<RequestedProducts>();

                List<Product> StoreProd = await _repository.GetListStockedProductsForStoreAsync(StoreID);
                foreach (Product BLProd in StoreProd)
                {
                    VMOrderView.Products.Add(new RequestedProducts()
                    {
                        ProductID = BLProd.productTypeID,
                        ProductAmount = BLProd.Amount,
                        ProductName = BLProd.name,
                        ProductPrice = BLProd.price
                    });
                }
                return View(VMOrderView);
            }
            else if (TempData["SelectedStore"] == null || TempData["LoggedCustomer"] == null)
            {
                if(TempData["SelectedStore"] == null)
                {
                    throw new Exception("There is no selected store in tempdata. You can only select a store to order from if logged in as a customer.");
                }
                else
                {
                    throw new Exception("There is no logged in customer within tempdata");
                }
            }
            else
            {
                throw new Exception("Something went wrong not related to the tempdata");
            }
        }
        // POST : Actually creates the order
        [HttpPost]
        public ActionResult CreatedOrder(CreateOrderViewModel VMOrderCart)
        {
            int CustomerID = int.Parse(TempData["LoggedCustomer"].ToString());
            int StoreID = int.Parse(TempData["SelectedStore"].ToString());
            Order BLOrd = new Order();


            BLOrd.Customer.customerID = CustomerID;
            BLOrd.StoreLocation.storeNumber = StoreID;

            foreach(var item in VMOrderCart.Products)
            {
                if (item.ProductAmount != 0)
                {
                    BLOrd.CustomerProductList.Add(new Product()
                    {
                        name = item.ProductName,
                        Amount = item.ProductAmount,
                        price = item.ProductPrice,
                        productTypeID = item.ProductID
                    });
                }
            }
            if (BLOrd.CustomerProductList.Count() == 0)
            {
                //invalid order
            }
            else
            {
                _repository.AddPlacedOrderToCustomerAsync(CustomerID, BLOrd);
            }

            TempData.Keep("LoggedCustomer");

            return View(VMOrderCart);
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