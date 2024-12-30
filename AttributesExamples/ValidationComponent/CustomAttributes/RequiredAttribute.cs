using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationComponent.CustomAttributes
{
    [AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : System.Attribute
    {
        public string ErrorMessage { get; set; }

        public RequiredAttribute()
        {
            ErrorMessage = "You cannot leave the {0} field empty";
        }
        public RequiredAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

    }
}
