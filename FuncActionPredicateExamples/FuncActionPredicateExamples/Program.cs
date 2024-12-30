using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncActionPredicateExamples
{
    class Program
    {
        delegate TResult Func2<out TResult>();
        delegate TResult Func2<in T1, out TResult>(T1 arg);
        delegate TResult Func2<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        delegate TResult Func2<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);

        static void Main(string[] args)
        {
            MathClass mathClass = new MathClass();

            //Func<int, int, int> calc = mathClass.Sum;
            //Func<int, int, int> calc = delegate (int a, int b) { return a + b; };
            //Func<int, int, int> calc = (a, b) => a + b;
            //Func2<int, int, int> calc = (a, b) => a + b;

            //int result = calc(1, 2);

            //Console.WriteLine("result=" + result);
            //Console.ReadLine();

            //float d = 2.3f, e = 4.56f;
            //int f = 5;

            //Func<float, float, int, float> calc2 = (arg1, arg2, arg3) => (arg1 + arg2) * arg3;
            //float result2 = calc2(d, e, f);

            //Console.WriteLine("result2 ="+result2);
            //Console.ReadLine();

            Func<decimal, decimal, decimal> calculateTotalAnnualSalary =
                  (annualSalary, bonusPercentage) => annualSalary + (annualSalary * (bonusPercentage / 100));

            //Console.WriteLine($"Total anual salary ={calculateTotalAnnualSalary(60000, 2)}");

            Action<int, string, string> displayEmployeeDetails = (arg1, arg2, arg3)
                => Console.WriteLine("Id=" + arg1 + Environment.NewLine + "First name=" + arg2 + Environment.NewLine + "Last name=" + arg3);

            //displayEmployeeDetails(1, "Sarah", "Jones");

            Action<int, string, string, decimal, char, bool> displayEmployeeDetails2 = (arg1, arg2, arg3, arg4, arg5, arg6)
                => Console.WriteLine("Id=" + arg1 + Environment.NewLine + "First name=" + arg2 + Environment.NewLine
                                   + "Last name=" + arg3 + Environment.NewLine + "Annual salary=" + arg4 + Environment.NewLine
                                   + "Gender=" + arg5 + Environment.NewLine + "IsManager=" + arg6);

            //displayEmployeeDetails2(1, "Sarah", "Jones", 60000,'f',true);


            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, FirstName = "Sarah", LastName = "Jones", AnnualSalary = calculateTotalAnnualSalary(60000,2), Gender = 'f', IsManager = true });
            employees.Add(new Employee { Id = 2, FirstName = "Sarah", LastName = "Wilson", AnnualSalary = calculateTotalAnnualSalary(48000,10), Gender = 'f', IsManager = false });
            employees.Add(new Employee { Id = 3, FirstName = "Peter", LastName = "Wilson", AnnualSalary = 80000, Gender = 'm', IsManager = true });
            employees.Add(new Employee { Id = 4, FirstName = "Robert", LastName = "Thompson", AnnualSalary = 9000, Gender = 'm', IsManager = false });
            employees.Add(new Employee { Id = 5, FirstName = "Anne", LastName = "Jones", AnnualSalary = 45000, Gender = 'f', IsManager = false });

            //List<Employee> employeesFiltered = FilterEmployees(employees, empl => empl.AnnualSalary > 50000);
            List<Employee> employeesFiltered = employees.FilterEmployees(empl => empl.AnnualSalary > 50000);
            //List<Employee> employeesFiltered = employees.Where(empl => empl.AnnualSalary > 50000).ToList();

            foreach (Employee employee in employeesFiltered)
            {
                displayEmployeeDetails2(employee.Id, employee.FirstName,employee.LastName,employee.AnnualSalary,employee.Gender,employee.IsManager);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        static List<Employee> FilterEmployees(List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> employeesFiltered = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                {
                    employeesFiltered.Add(employee);
                }
            }
            return employeesFiltered;
        }

    }

    public static class Extensions
    {
        public static List<Employee> FilterEmployees(this List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> employeesFiltered = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                {
                    employeesFiltered.Add(employee);
                }
            }
            return employeesFiltered;
        }

    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public char Gender { get; set; }
        public bool IsManager { get; set; }

    }

    public class MathClass
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
