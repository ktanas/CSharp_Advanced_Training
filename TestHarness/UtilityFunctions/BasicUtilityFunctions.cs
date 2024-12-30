using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityFunctions
{
    [InformationAttribute(Description = "This class contains utility methods")]
    public class BasicUtilityFunctions
    {
        [InformationAttribute(Description = "This method returns a welcome message")]
        public string WriteWelcomeMessage()
        {
            return "Welcome to BasicUtilityFunctions class";
        }
        [InformationAttribute(Description = "This method computes sum of two integers")]
        public int IntegerPlusInteger(int a, int b)
        {
            return a + b;
        }

        [InformationAttribute(Description = "This method concatenates three strings")]
        public string ConcatThreeStrings (string s1, string s2, string s3)
        {
            return s1 + " " + s2 + " " + s3;
        }

        [InformationAttribute(Description = "This method returns length of a string")]
        public int GetStringLength(string stringValue)
        {
            return stringValue.Length;
        }

    }
}
