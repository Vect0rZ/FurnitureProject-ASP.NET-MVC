using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FurnitureProject.Common.Services;

namespace FurnitureProject.Common
{
    public class BaseController : Controller
    {
        private CustomerService customerService = null;

        public CustomerService CustomerService {
            get
            {
                if(customerService == null)
                {
                    customerService = new CustomerService();
                }
                return customerService;
            } 
        }

    }
}