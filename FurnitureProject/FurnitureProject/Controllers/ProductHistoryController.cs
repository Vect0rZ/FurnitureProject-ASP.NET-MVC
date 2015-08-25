using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FurnitureProject.Common;
using FurnitureProject.Models;
using FurnitureProject.Common.Services;

namespace FurnitureProject.Controllers
{
    [Authorize]
    public class ProductHistoryController : BaseController
    {
        // GET: ProductHistory
        public ActionResult Index(ProductHistoryIndexVM model)
        {
            var res = ProductHistoryService.GetProductsForCustomer(model.SearchString);

            model.CustomerProducts = res.Data;
            model.ErrorMessage = res.Error;

            return View(model);
        }
    }
}