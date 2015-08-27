using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;
using FurnitureProject.Common.Services;
using FurnitureProject.Common.Services.OrderService;


namespace FurnitureProject.Models
{
    public class OrdersSearchOrderVM
    {
        public OrderAndProducts OrderAndProducts { get; set; }
        public int? OrderID { get; set; }
        public string Message { get; set; }
        public OrdersSearchOrderVM()
        {
            OrderAndProducts = new OrderAndProducts();
        }
    }
}