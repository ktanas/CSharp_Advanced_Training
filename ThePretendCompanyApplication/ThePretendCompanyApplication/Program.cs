using System;
using System.Collections.Generic;
using TCPData;
using TCPExtensions;
using System.Linq;

namespace ThePretendCompanyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Employee> employeeList = Data.GetEmployees();

            ////var filteredEmployees = employeeList.Filter(empl => (empl.IsManager == false) && (empl.AnnualSalary > 50000.0m));
            //var filteredEmployees = employeeList.Where(empl => (empl.IsManager == false) && (empl.AnnualSalary > 50000.0m));

            //foreach (var employee in filteredEmployees)
            //{
            //    Console.WriteLine("First name:"+employee.FirstName
            //                     +",Last name:"+employee.LastName
            //                     +",AnnualSalary:"+employee.AnnualSalary
            //                     +",IsManager:"+employee.IsManager
            //                     +",DepartmentId:"+employee.DepartmentId);
            //}

            //Console.WriteLine();

            //List<Department> departmentList = Data.GetDepartments();

            ////var filteredDepartments = departmentList.Filter(dep => dep.ShortName.IndexOf('R') != -1);
            //var filteredDepartments = departmentList.Where(dep => dep.ShortName.IndexOf('R') != -1);

            //foreach (var department in filteredDepartments)
            //{
            //    Console.WriteLine("Short name:" + department.ShortName
            //                     + ",Long name:" + department.LongName);
            //}

            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var resultList = from emp in employeeList
                             join dep in departmentList
                             on emp.DepartmentId equals dep.Id
                             select new
                             {
                                 FirstName = emp.FirstName,
                                 LastName = emp.LastName,
                                 Salary = emp.AnnualSalary,
                                 Manager = emp.IsManager,
                                 Department = dep.LongName
                             };

            var averageAnnualSalary = resultList.Average(a => a.Salary);
            var highestAnnualSalary = resultList.Max(a => a.Salary);
            var lowestAnnualSalary = resultList.Min(a => a.Salary);

            Console.WriteLine("Highest annual salary:" + highestAnnualSalary);
            Console.WriteLine("Lowest annual salary:" + lowestAnnualSalary);
            Console.WriteLine("Average annual salary:" + averageAnnualSalary);


            Console.ReadLine();
        }
    }
}
