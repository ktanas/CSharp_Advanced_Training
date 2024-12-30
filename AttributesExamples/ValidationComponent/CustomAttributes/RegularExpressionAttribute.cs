using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationComponent.CustomAttributes
{
    [AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class RegularExpressionAttribute:System.Attribute
    {
        public string ErrorMessage { get; set; }
        public string Pattern { get; set; }

        public RegularExpressionAttribute(string pattern)
        {
            Pattern = pattern;
            ErrorMessage = "Field {0} is invalid. The provided value does not match declared regular expression pattern, {1}";
        }
        public RegularExpressionAttribute(string pattern, string errorMessage)
        {
            Pattern = pattern;
            ErrorMessage = errorMessage;
        }
    }
}
