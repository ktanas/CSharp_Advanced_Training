using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Salaries salaries = new Salaries();

            //ArrayList salaryList = salaries.GetSalaries();
            List<float> salaryList = salaries.GetSalaries();

            //float salary = (float)salaryList[1]; // float is a 'System.Single' type, not compatible with 'System.Double'
            // we need to define values as '60000.34f' instead of '60000.34' or use generics

            float salary = salaryList[1];
            salary = salary * 1.02f; // let us define a bonus

            Console.WriteLine("salary=" + salary);
            Console.ReadLine();

        }
    }

    public class Salaries
    {
        //ArrayList _salaryList = new ArrayList();
        List<float> _salaryList = new List<float>();

        public Salaries()
        {
            /*
            _salaryList.Add(60000.34);
            _salaryList.Add(40000.51);
            _salaryList.Add(20000.23);
            */
            _salaryList.Add(60000.34f);
            _salaryList.Add(40000.51f);
            _salaryList.Add(20000.23f);
        }

        //public ArrayList GetSalaries()
        public List<float> GetSalaries()
        {
            return _salaryList;
        }
    }
}
