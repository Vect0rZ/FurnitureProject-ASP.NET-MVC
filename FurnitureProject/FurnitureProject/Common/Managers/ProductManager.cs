using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Managers
{
    public class ProductManager
    {
        public static bool ValidateProduct(Product product)
        {
            if(product.Barcode == null
               || String.IsNullOrWhiteSpace(product.Name))
            {
                return false;
            }

            return true;
        }
    }
}