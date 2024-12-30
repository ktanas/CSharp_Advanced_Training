using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityFunctions
{
    [InformationAttribute(Description = "This class contains basic math functions")]

    public class BasicMathFunctions
    {
        [InformationAttribute(Description = "This method divides two numbers of 'double' type")]

        public double DivideOperation(double number1, double number2)
        {
            return number1 / number2;
        }
        [InformationAttribute(Description = "This method multiplies two numbers of 'double' type")]
        public double MultiplyOperation(double number1, double number2)
        {
            return number1 * number2;
        }
    }
}
