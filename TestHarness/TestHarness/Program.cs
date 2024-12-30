using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            const string TargetAssemblyFileName = "UtilityFunctions.dll";
            const string TargetNamespace = "UtilityFunctions";

            Assembly assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TargetAssemblyFileName));

            //List<System.Type> classes = assembly.GetTypes().Where(t => t.Namespace == TargetNamespace && HasInformationAttribute(t)).ToList();
            List<System.Type> classes = assembly.GetTypes().Where(t => t.Namespace == TargetNamespace).ToList();

            Console.WriteLine("Select class number");

            //int count = 0;

            //foreach (var classItem in classes)
            //{
            //    count++;
            //    Console.WriteLine(count + "." + classItem);
            //}

            DisplayProgramElementList(classes);

            Type typeChoice = ReturnProgramElementReferenceFromList<Type>(classes);

            object classInstance = Activator.CreateInstance(typeChoice,null);

            Console.Clear();

            WriteHeadingToScreen("Class: " + typeChoice);

            WritePromptToScreen("Select method number");

            //List<MethodInfo> methods = typeChoice.GetMethods().Where(m => HasInformationAttribute(m)).ToList();
            List<MethodInfo> methods = typeChoice.GetMethods().ToList();

            DisplayProgramElementList<MethodInfo>(methods);

            MethodInfo methodChoice = ReturnProgramElementReferenceFromList<MethodInfo>(methods);

            if (methodChoice != null)
            {
                Console.Clear();
                WriteHeadingToScreen("Class: " + typeChoice + " ,Method: " + methodChoice.Name);

                ParameterInfo[] parameters = methodChoice.GetParameters();

                object result = GetResult(classInstance, methodChoice, parameters);

                WriteResultToScreen(result);
            }


            //ConsoleKey consoleKey = Console.ReadKey().Key;
            //Type classChoice = null;

            //switch (consoleKey)
            //{
            //    case ConsoleKey.D1:
            //        classChoice = classes[0];
            //        break;
            //    case ConsoleKey.D2:
            //        classChoice = classes[1];
            //        break;
            //    case ConsoleKey.D3:
            //        classChoice = classes[2];
            //        break;
            //    case ConsoleKey.D4:
            //        classChoice = classes[3];
            //        break;
            //}
            //
            //object classInstance = Activator.CreateInstance(classChoice,null);
            //
            //Console.Clear();
            //
            //WriteHeadingToScreen("Class:" + classChoice);
            //WritePromptToScreen("Select method number");

        }

        private static bool HasInformationAttribute(MemberInfo mi)
        {
            const string InformationAttributeTypeName = "UTILITYFUNCIONS.INFORMATIONATTRIBUTE";

            foreach (var attrib in mi.GetCustomAttributes())
            {
                Type typeAttrib = attrib.GetType();

                if (typeAttrib.ToString().ToUpper() == InformationAttributeTypeName)
                {
                    return true;
                }
            }
            return false;
        }

        private static void WriteResultToScreen(object result)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Result: " + result);
            Console.ResetColor();
            Console.WriteLine();
        }
        private static object[] ReturnParameterValueInputAsObjectArray(ParameterInfo[] parameters)
        {
            object[] paramValues = new object[parameters.Length];
            int itemCount = 0;

            foreach(ParameterInfo parameterInfo in parameters)
            {
                WritePromptToScreen("Enter value of " + parameterInfo.Name + " parameter");

                if (parameterInfo.ParameterType == typeof(string))
                {
                    string inputString = Console.ReadLine();
                    paramValues[itemCount] = inputString;
                }
                else if (parameterInfo.ParameterType == typeof(int))
                {
                    int inputInt = Int32.Parse(Console.ReadLine());
                    paramValues[itemCount] = inputInt;
                }
                else if (parameterInfo.ParameterType == typeof(double))
                {
                    double inputDouble = Double.Parse(Console.ReadLine());
                    paramValues[itemCount] = inputDouble;
                }
                itemCount++;
            }
            return paramValues;
        }
        private static object GetResult(Object classInstance, MethodInfo methodInfo, ParameterInfo[] parameters)
        {
            object result = null;

            if (parameters.Length == 0)
            {
                result = methodInfo.Invoke(classInstance, null);
            }
            else
            {
                var paramValueArray = ReturnParameterValueInputAsObjectArray(parameters);
                result = methodInfo.Invoke(classInstance, paramValueArray);
            }
            return result;


        
        }
        private static T ReturnProgramElementReferenceFromList<T>(List<T> list)
        {
            ConsoleKey consoleKey = Console.ReadKey().Key;
            
            switch (consoleKey)
            {
                case ConsoleKey.D1:
                    return list[0];
                case ConsoleKey.D2:
                    return list[1];
                case ConsoleKey.D3:
                    return list[2];
                case ConsoleKey.D4:
                    return list[3];
                case ConsoleKey.D5:
                    return list[4];
                case ConsoleKey.D6:
                    return list[5];
                case ConsoleKey.D7:
                    return list[6];
            }
            return default;
        }

        private static void DisplayProgramElementList<T>(List<T> list)
        {
            int count = 0;

            foreach (var item in list)
            {
                count++;
                Console.WriteLine(count + "." + item);

            }
        }

        private static void WriteHeadingToScreen(string heading)
        {
            Console.WriteLine(heading);
            Console.WriteLine(new string('-', heading.Length));
            Console.WriteLine();
        }

        private static void WritePromptToScreen(string promptText)
        {
            Console.WriteLine(promptText);
        }
    }
}
