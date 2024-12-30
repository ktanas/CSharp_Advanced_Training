using System;
using System.Diagnostics.CodeAnalysis;

namespace GenericBubbleSortApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] a = new int[] { 17, -12, 6, -1, 0, 2, -9, 15 };
            //object[] a = new object[] { 17, -12, 6, -1, 0, 2, -9, 15 };
            string[] b = new string[] { "Adam", "Peter", "Catherine", "Thomas", "Robert", "Pauline", "John" };
            //object[] b = new object[] { "Adam", "Peter", "Catherine", "Thomas", "Robert", "Pauline", "John" };

            /*
            Employee[] c = new Employee[]
            {
                new Employee {Id=5, Name="Anne Roberts"},
                new Employee {Id=3, Name="Paul Peterson"},
                new Employee {Id=4, Name="Jack Collins"},
                new Employee {Id=2, Name="Anne Smith"},
                new Employee {Id=1, Name="Jack Adams"}
            };
            */
            //SortArray sortArray = new SortArray();
            //SortArray<Employee> sortArray = new SortArray<Employee>();
            //SortArray<int> sortArray = new SortArray<int>();
            SortArray<string> sortArray = new SortArray<string>();

            /*
            sortArray.BubbleSort(a);

            foreach (var element in a)
                Console.WriteLine(element);
            Console.ReadLine();
            */
            sortArray.BubbleSort(b);
            
            foreach (var element in b)
                Console.WriteLine(element);
            Console.ReadLine();
            /*
            sortArray.BubbleSort(c);

            foreach (var element in c)
                Console.WriteLine(element);
            Console.ReadLine();
            */
        }
    }

    //public class Employee : IComparable
    public class Employee : IComparable<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CompareTo([AllowNull] Employee other)
        {
            //return this.Id.CompareTo(other.Id);
            return this.Name.CompareTo(other.Name);
        }

        /*
        public int CompareTo(object obj)
        {
            return this.Id.CompareTo(((Employee)obj).Id);
        }
        */
        public override string ToString()
        {
            //return base.ToString();
            return Id + " " + Name;
        }

    }

    //public class SortArray<T> where T:IComparable
    public class SortArray<T> where T:IComparable<T>
    {
        public void BubbleSort(T[] a)
        {
            int n = a.Length;

            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    //if (a[j] > a[j+1])
                    //if (((IComparable)a[j]).CompareTo(a[j + 1]) > 0)
                    if (a[j].CompareTo(a[j+1]) > 0)
                    {
                        Swap(a, j);
                    }

        }

        private void Swap(T[] a, int j)
        {
            T x = a[j];
            a[j] = a[j + 1];
            a[j + 1] = x;
        }
    }


    //public class SortArray
    //{
    //    public void BubbleSort(object[] a)
    //    {
    //        int n = a.Length;

    //        for (int i=0; i<n-1; i++)
    //            for (int j=0; j<n-i-1; j++)
    //                //if (a[j] > a[j+1])
    //                if (((IComparable)a[j]).CompareTo(a[j+1]) > 0)
    //                {
    //                    Swap(a, j);
    //                }

    //    }

    //    private void Swap(object[] a, int j)
    //    {
    //        object x = a[j];
    //        a[j] = a[j + 1];
    //        a[j + 1] = x;
    //    }
    //}
}
