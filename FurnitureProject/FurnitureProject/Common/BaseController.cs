using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FurnitureProject.Common.Services;
using FurnitureProject.Common.Services.OrderService;

namespace FurnitureProject.Common
{
    public class BaseController : Controller
    {
        private CustomerService customerService = null;
        private OrderService orderService = null;
        private ProductService productService = null;

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

        public OrderService OrderService
        {
            get
            {
                if(orderService == null)
                {
                    orderService = new OrderService();
                }

                return orderService;
            }
        }

        public ProductService ProductService
        {
            get
            {
                if(productService == null)
                {
                    productService = new ProductService();
                }

                return productService;
            }
        }

    }
}