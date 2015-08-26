using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using FurnitureProject.Common;
using FurnitureProject.Models;

namespace FurnitureProject.Controllers
{
    public class CustomersController : BaseController
    {
        public ActionResult Index(CustomerIndexVM model)
        {
            model.PageNumber = CustomerService.ValidatePageNumber(model.PageNumber);
            model.RowsPerPage = CustomerService.ValidateRowsPerPage(model.RowsPerPage);
            model.NumberOfPages = CustomerService.GetNumberOfPages(model.RowsPerPage);
            model.Customers = CustomerService.GetAllOnPage(model.PageNumber, model.RowsPerPage);

            return View("Index", model);
        }

    }
}
