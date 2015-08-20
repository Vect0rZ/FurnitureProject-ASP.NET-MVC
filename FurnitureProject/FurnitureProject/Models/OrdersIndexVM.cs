using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;
using FurnitureProject.Common.Services.OrderService;

namespace FurnitureProject.Models
{
    public class OrdersIndexVM
    {
        //public Dictionary<Order, float> Orders = new Dictionary<Order, float>();
        public List<OrderWithTotalPrice> Orders { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
    }
}