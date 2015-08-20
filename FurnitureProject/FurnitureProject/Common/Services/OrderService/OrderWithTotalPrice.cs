using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services.OrderService
{
    public class OrderWithTotalPrice
    {
        public Order Order { get; set; }
        public float TotalPrice { get; set; }
    }
}