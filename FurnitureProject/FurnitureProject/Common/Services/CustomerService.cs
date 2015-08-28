using FurnitureProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common.Managers;
using FurnitureProject.Common.Helpers;

namespace FurnitureProject.Common.Services
{
    public class CustomerService
    {
        private FurnitureDBContext context;

        public CustomerService()
        {
            context = new FurnitureDBContext();
        }

        public int ValidatePageNumber(int pageNumber)
        {
            if(pageNumber < 0)
            {
                return 0;
            }

            return pageNumber;
        }

        public bool CustomerExists(int customerID)
        {
            var query = context.Customers.Where(c => c.ID == customerID);

            if (query.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public int ValidateRowsPerPage(int rowsPerPage)
        {
            if(rowsPerPage <= 0 || rowsPerPage % 25 != 0)
            {
                return 25;
            }

            return rowsPerPage;
        }

        public Customer GetCustomerByID(int? customerId)
        {
            Customer result = null; ;

            if(customerId != null)
            {
                result = context.Customers.Find(customerId);
            }

            return result;
        }

        public int GetNumberOfPages(int rowsPerPage)
        {
            return context.Customers.Count() / rowsPerPage;
        }

        public List<Customer> GetAllOnPage(int pageNumber, int rowsPerPage)
        {
            var customers = context.Customers.OrderBy(c => c.MOL);
            return customers.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
        }

        public EntityTaskResult<Customer> AddCustomer(AddCustomerViewModel customer)
        {

            Customer resultCustomer = new Customer()
            {
                Bulstat = customer.Bulstat,
                Name = customer.Name,
                MOL = customer.MOL,
                Address = customer.Address
            };

            if(CustomerManager.ValidateCustomer(resultCustomer) == false)
            {
                return new EntityTaskResult<Customer>().SetErrorMessage("Not a valid customer")
                                                       .SetSuccess(false);
            }
            if (CheckForDuplicateBulstat(resultCustomer) == true)
            {
                return new EntityTaskResult<Customer>().SetErrorMessage("There is allready a customer with such Bulstat")
                                                       .SetSuccess(false);
            }
            context.Customers.Add(resultCustomer);
            
            if(context.SaveChanges() > 0)
            {
                return new EntityTaskResult<Customer>().SetData(resultCustomer)
                                                       .SetSuccess(true);
            }

            return new EntityTaskResult<Customer>().SetErrorMessage("Something went wrong.").SetSuccess(false);
        }

        public bool DeleteCustomer(int? customerId)
        {
            bool result = false;

            Customer customer = GetCustomerByID(customerId);

            context.Customers.Remove(customer);

            int changes = context.SaveChanges();

            if(changes > 0)
            {
                result = true;
            }

            return result;
        }

        public AddCustomerViewModel GetCustomerViewModel(int customerId)
        {
            AddCustomerViewModel result = null;
            Customer customer = GetCustomerByID(customerId);

            if(customer != null)
            {
                result = new AddCustomerViewModel()
                {
                    CustomerID = customer.ID,
                    Bulstat = customer.Bulstat,
                    Name = customer.Name,
                    MOL = customer.MOL,
                    Address = customer.Address
                };
            }

            return result;
        }

        public EntityTaskResult<Customer> EditCustomer(AddCustomerViewModel model)
        {
            Customer result = GetCustomerByID(model.CustomerID);

            if(result == null)
            {
                return new EntityTaskResult<Customer>().SetErrorMessage("No such user found.")
                                                       .SetSuccess(false);
            }

            result.Bulstat = model.Bulstat;
            result.Name = model.Name;
            result.MOL = model.MOL;
            result.Address = model.Address;

            if(CheckForDuplicateBulstat(result, result.ID) == true)
            {
                return new EntityTaskResult<Customer>().SetErrorMessage("Bulstat already exists")
                                                       .SetSuccess(false);
            }

            context.Customers.Attach(result);
            context.Entry(result).State = System.Data.Entity.EntityState.Modified;

            int changes = context.SaveChanges();

            if(changes > 0)
            {
                return new EntityTaskResult<Customer>().SetData(result).SetSuccess(true);
            }

            return new EntityTaskResult<Customer>().SetErrorMessage("Something went wrong")
                                                   .SetSuccess(false);
            
        }
        #region private methods

        private bool CheckForDuplicateBulstat(Customer customer, int? excludeId = null)
        {
            bool result = false;
            Customer existingCustomer = context.Customers.FirstOrDefault(c => c.Bulstat == customer.Bulstat);

            if(existingCustomer != null)
            {
                result = true;
            }
            if(existingCustomer.ID == excludeId)
            {
                result = false;
            }
            return result;
        }

        #endregion
    }
}