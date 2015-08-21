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
        public OrderWithTotalPrice Order { get; set; }
        public Customer Customer { get; set; }
        public List<ProductWithQuantity> Products { get; set; }
        public float TotalPrice { get; set; }
        public int? OrderID { get; set; }

        public OrdersSearchOrderVM()
        {
            Products = new List<ProductWithQuantity>();
            Order = new OrderWithTotalPrice();
        }
    }
}