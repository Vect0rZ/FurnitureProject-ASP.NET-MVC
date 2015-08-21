using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services.OrderService
{
    public class OrderWithProducts
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public List<ProductWithQuantity> Products { get; set; }
        public int OrderID { get; set; }
    }
}