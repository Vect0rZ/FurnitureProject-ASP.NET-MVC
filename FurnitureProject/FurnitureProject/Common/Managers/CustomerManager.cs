using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace FurnitureProject.Common.Managers
{
    public class CustomerManager
    {
        public static bool ValidateCustomer(Customer customer)
        {
            if( customer.Address == null
                || customer.MOL == null
                || customer.Name == null)
            {
                return false;
            }

            return true;
        }
    }
}