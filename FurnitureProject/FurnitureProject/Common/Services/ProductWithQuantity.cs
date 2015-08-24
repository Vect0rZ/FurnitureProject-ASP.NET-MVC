using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common.Services.OrderService;

namespace FurnitureProject.Common.Services
{
    public class ProductWithQuantity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
        public float TotalPrice { get; set; }
    }
}