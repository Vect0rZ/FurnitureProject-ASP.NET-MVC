using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services.OrderService
{
    public class OrderAndProducts
    {
        public OrderWithTotalPrice Order { get; set; }
        public List<ProductWithQuantity> Products { get; set; }

        public OrderAndProducts()
        {
            Order = new OrderWithTotalPrice();
            Products = new List<ProductWithQuantity>();
        }
    }
}