using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;

namespace FurnitureProject.Models
{
    public class ProductsIndexVM
    {
        public List<Product> Products { get; set; }
        public string SearchString { get; set; }
        public float Price { get; set; }
        public bool isLessThan { get; set; }
    }
}