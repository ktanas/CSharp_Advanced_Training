#define LOG_INFO
using System;
using System.Reflection;
using LoggingComponent;
using ValidationComponent;
using AttributesExamples.Models;
using System.Text.Json;

//[assembly: AssemblyVersion("2.0.1")]

[assembly: AssemblyDescription("My Assembly Description")]


namespace AttributesExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee();
            Department dep = new Department();

            string empId = null;
            string firstName = null;
            string postCode = null;
            string deptShortName = null;

            Type employeeType = typeof(Employee);
            Type departmentType = typeof(Department);

            if (GetInput(employeeType, "Please enter employee's Id","Id", out empId))
            {
                emp.Id = Int32.Parse(empId);
            }
            if (GetInput(employeeType, "Please enter employee's first name", "FirstName", out firstName))
            {
                emp.FirstName = firstName;
            }

            if (GetInput(employeeType, "Please enter employee's post code", "PostCode", out postCode))
            {
                emp.PostCode = postCode;
            }
            if (GetInput(departmentType, "Please enter employee's department short name", "DeptShortName", out deptShortName))
            {
                dep.DeptShortName = deptShortName;
            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("New employee created. Id=" + emp.Id + ",firstName = " + emp.FirstName);
            Console.ResetColor();
            Console.ReadLine();

            var employeeJSON = JsonSerializer.Serialize<Employee>(emp);

            Console.WriteLine(employeeJSON);
            Console.ReadLine();

        }

        private static bool GetInput(Type t, string promptText, string fieldName, out string fieldValue)
        {
            fieldValue = "";
            string enteredValue = "";
            string errorMessage = null;

            do
            {
                Console.WriteLine(promptText);
                enteredValue = Console.ReadLine();
                if (!Validation.PropertyValueIsValid(t,enteredValue,fieldName, out errorMessage))
                {
                    fieldValue = null;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(errorMessage);
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else
                {
                    fieldValue = enteredValue;
                    break;
                }
            }
            while (true);

            return true;
        }

        private static void LoggingTest()
        {
            Logging.LogToScreen("My name is Bond, James Bond");
            Logging.LogToFile("My name is Bond, James Bond");
            Console.ReadLine();
        }
        private static void OutputGlobalAttributeInformation()
        {
            Assembly thisAssem = typeof(Program).Assembly;

            AssemblyName thisAssemName = thisAssem.GetName();

            Version thisAssemVersion = thisAssemName.Version;

            object[] attributes = thisAssem.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

            var thisAssemDescriptionAttribute = attributes[0] as AssemblyDescriptionAttribute;

            Console.WriteLine("Assembly Name: " + thisAssemName + ",Assembly Version: " + thisAssemVersion);

            if (thisAssemDescriptionAttribute != null)
                Console.WriteLine("Assembly Description: " + thisAssemDescriptionAttribute.Description);

            Console.ReadLine();
        }
    }
}
