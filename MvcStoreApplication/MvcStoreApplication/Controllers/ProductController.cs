using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStoreApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly BusinessLogic _businessLogic;

        public ProductController(BusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            if (_businessLogic.CurrentUserIsAdministrator())
            {
                return View(_businessLogic.GetAllProductViewModels());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            if (_businessLogic.CurrentUserIsAdministrator())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            if (_businessLogic.CurrentUserIsAdministrator())
            {
                if (_businessLogic.CreateNewProduct(product))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
