using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Models
{
    /// <summary>
    /// Since now most of the validation passes data through EntityTaskResult
    /// we should be able to set an error message to the user interface,
    /// wich is common for all ViewModels that require strong validation.
    /// The error message is ought to be set mostly thorught the action methods
    /// </summary>
    public class BaseViewModel
    {
        public string ErrorMessage { get; set; }
    }
}