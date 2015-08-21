using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;
using FurnitureProject.Common.Services;

namespace FurnitureProject.Models
{
    public class OrdersProductsVM
    {
        public List<ProductWithQuantity> Products { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        public int OrderID { get; set; }
    }
}