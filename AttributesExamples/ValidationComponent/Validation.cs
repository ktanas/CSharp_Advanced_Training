using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading.Tasks;
using ValidationComponent.CustomAttributes;

namespace ValidationComponent
{
    public static class Validation
    {
        public static bool PropertyValueIsValid(Type t, string enteredValue, string elementName, out string errorMessage)
        {
            PropertyInfo propInfo = t.GetProperty(elementName);

            Attribute[] attributes = propInfo.GetCustomAttributes().ToArray();

            errorMessage = "";

            foreach (Attribute attr in attributes)
            {
                switch (attr)
                {
                    case RequiredAttribute ra:
                        if (!FieldRequiredIsValid(enteredValue))
                        {
                            errorMessage = ra.ErrorMessage;
                            errorMessage = string.Format(errorMessage, propInfo.Name);
                            return false;
                        }
                        break;
                    case StringLengthAttribute sla:
                        if (!FieldStringLengthIsValid(sla, enteredValue))
                        {
                            errorMessage = sla.ErrorMessage;
                            errorMessage = string.Format(errorMessage, propInfo.Name, sla.MinLength, sla.MaxLength);
                            return false;
                        }
                        break;
                    case RegularExpressionAttribute rea:
                        if (!FieldPatternMatchIsValid(rea, enteredValue))
                        {
                            errorMessage = rea.ErrorMessage;
                            errorMessage = string.Format(errorMessage, propInfo.Name, rea.Pattern);
                            return false;
                        }
                        break;

                }
            }
            return true;
        }
        private static bool FieldRequiredIsValid(string enteredValue)
        {
            if (!string.IsNullOrEmpty(enteredValue))
                return true;

            return false;
        }
        private static bool FieldStringLengthIsValid(StringLengthAttribute stringLengthAttribute, string enteredValue)
        {
            if ((enteredValue.Length >= stringLengthAttribute.MinLength) && (enteredValue.Length <= stringLengthAttribute.MaxLength))
                return true;

            return false;
        }

        private static bool FieldPatternMatchIsValid(RegularExpressionAttribute regularExpressionAttribute, string enteredValue)
        {
            if (Regex.IsMatch(enteredValue, regularExpressionAttribute.Pattern))
                return true;

            return false;
        }
    }
}
