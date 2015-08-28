using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Helpers
{
    public class EntityTaskResult<T> where T : class
    {
        public string ErrorMessage { get; private set; }
        public bool Success { get; private set; }
        public T Data { get; private set; }

        public EntityTaskResult<T> SetSuccess(bool success)
        {
            Success = success;

            return this;
        }

        public EntityTaskResult<T> SetData(T data)
        {
            Data = data;

            return this;
        }

        public EntityTaskResult<T> SetErrorMessage(string message)
        {
            ErrorMessage = message;

            return this;
        }
    }
}