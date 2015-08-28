using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

using FurnitureProject.Common;

namespace FurnitureProject.Models
{
    public class AddCustomerViewModel : BaseViewModel
    {
        public int? CustomerID { get; set; }

        [Required]
        [Display(Name="Bulstat")]
        [Range(0, Int32.MaxValue, ErrorMessage="Invalid bulstat")]
        public int Bulstat { get; set; }

        [Required]
        [Display(Name="Name")]
        [DataType(DataType.Text, ErrorMessage="Invalid company input.")]
        public string Name { get; set; }

        [Required]
        [Display(Name="MOL")]
        [DataType(DataType.Text, ErrorMessage = "Invalid MOL name input.")]
        public string MOL { get; set; }

        [Required]
        [Display(Name="Address")]
        [DataType(DataType.Text, ErrorMessage = "Invalid address input.")]
        public string Address { get; set; }

        public AddCustomerViewModel()
        {

        }

        public AddCustomerViewModel(Customer customer, string errorMessage = null)
        {
            CustomerID = customer.ID;
            Bulstat = customer.Bulstat;
            Name = customer.Name;
            MOL = customer.MOL;
            Address = customer.Address;

            if(errorMessage != null)
            {
                ErrorMessage = errorMessage;
            }
        }
    }
}