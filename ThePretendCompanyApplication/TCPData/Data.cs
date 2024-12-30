using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPData
{
    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                AnnualSalary = 25000.0m,
                IsManager = false,
                DepartmentId = 1
            });

            employees.Add(new Employee
            {
                Id = 2,
                FirstName = "Pauline",
                LastName = "Wilkins",
                AnnualSalary = 130000.4m,
                IsManager = true,
                DepartmentId = 1
            });

            employees.Add(new Employee
            {
                Id = 3,
                FirstName = "Monica",
                LastName = "Smith",
                AnnualSalary = 80000.9m,
                IsManager = true,
                DepartmentId = 2
            });

            employees.Add(new Employee
            {
                Id = 4,
                FirstName = "Adam",
                LastName = "Jones",
                AnnualSalary = 66666.6m,
                IsManager = false,
                DepartmentId = 2
            });

            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            departments.Add(new Department
            {
                Id = 1,
                ShortName = "ACC",
                LongName = "Accounting"
            });

            departments.Add(new Department
            {
                Id = 2,
                ShortName = "R&D",
                LongName = "Research and Development"
            });

            departments.Add(new Department
            {
                Id = 3,
                ShortName = "PROD",
                LongName = "Production"
            });

            return departments;
        }
    }
}
