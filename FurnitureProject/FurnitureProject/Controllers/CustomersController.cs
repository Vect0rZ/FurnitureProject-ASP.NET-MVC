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
        [HttpGet]
        public ActionResult Index(CustomerIndexVM model)
        {
            model.PageNumber = CustomerService.ValidatePageNumber(model.PageNumber);
            model.RowsPerPage = CustomerService.ValidateRowsPerPage(model.RowsPerPage);
            model.NumberOfPages = CustomerService.GetNumberOfPages(model.RowsPerPage);
            model.Customers = CustomerService.GetAllOnPage(model.PageNumber, model.RowsPerPage);

            return View("Index", model);
        }

        [Authorize]
        public ActionResult AddCustomer(AddCustomerViewModel customer)
        {
            if(ModelState.IsValid == false)
            {
                return View(customer);
            }
            var result = CustomerService.AddCustomer(customer);
            if(result.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                customer.ErrorMessage = result.ErrorMessage;
            }

            return View(customer);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCustomer(int? custId, AddCustomerViewModel model)
        {
            if(custId.HasValue == true) // So we come from the customer page
            {
                model = CustomerService.GetCustomerViewModel(custId.Value);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if(custId.HasValue == false) // Else we come from the customer Edit page with the newely edited model
            {
                var result = CustomerService.EditCustomer(model);

                if(result.Success == true)
                {
                    return RedirectToAction("Index");
                }

                model.ErrorMessage = result.ErrorMessage;
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCustomer(int? customerId)
        {
            if(CustomerService.DeleteCustomer(customerId))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
