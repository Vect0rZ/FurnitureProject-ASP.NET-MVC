﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;
using FurnitureProject.Common.Services;

namespace FurnitureProject.Models
{
    public class ProductsIndexVM
    {
        public List<ProductInfo> Products { get; set; }
        public string SearchString { get; set; }
        public float? Price { get; set; }
        public bool IsLessThan { get; set; }
        public int? OrderID { get; set; }

        public ProductsIndexVM()
        {
            SearchString = string.Empty;
        }
    }
}