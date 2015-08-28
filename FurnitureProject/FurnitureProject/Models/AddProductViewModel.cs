using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace FurnitureProject.Models
{
    public class AddProductViewModel : BaseViewModel
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name="Barcode")]
        [Range(0, Int32.MaxValue, ErrorMessage="Invalid barcode input.")]
        public int Barcode { get; set; }

        [Required]
        [Display(Name="Name")]
        [DataType(DataType.Text, ErrorMessage = "Invalid name input.")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Description")]
        [DataType(DataType.Text, ErrorMessage = "Invalid description input.")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Weight")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid weight input.")]
        public int Weight { get; set; }

        [Required]
        [Display(Name="Price")]
        [Range(0, float.MaxValue, ErrorMessage = "Invalid price input.")]
        public float Price { get; set; }

    }
}