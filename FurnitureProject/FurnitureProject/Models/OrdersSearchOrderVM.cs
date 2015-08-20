using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;
using FurnitureProject.Common.Services.OrderService;

namespace FurnitureProject.Models
{
    public class OrdersSearchOrderVM
    {
        public List<OrderWithProducts> Orders;
        public int OrderID { get; set; }
    }
}