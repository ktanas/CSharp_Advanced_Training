using System;
using System.IO;

namespace CovarianceAndContravarianceDelegateExample
{
    class Program
    {
        delegate Car CarFactoryDel(int id, string name);
        delegate void LogICECarDetailsDel(ICECar car);
        delegate void LogEVCarDetailsDel(EVCar car);

        static void Main(string[] args)
        {
            CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;

            Car car1 = carFactoryDel(1, "Audi R8");
            //Console.WriteLine($"Object type: {car1.GetType()}");
            //Console.WriteLine($"Car Details: {car1.GetCarDetails()}");

            carFactoryDel = CarFactory.ReturnEVCar;

            Console.WriteLine();

            Car car2 = carFactoryDel(2, "Tesla Model-3");
            //Console.WriteLine($"Object type: {car2.GetType()}");
            //Console.WriteLine($"Car Details: {car2.GetCarDetails()}");

            LogICECarDetailsDel iceLog = LogCarDetails;
            iceLog(car1 as ICECar);

            LogEVCarDetailsDel evLog = LogCarDetails;
            evLog(car2 as EVCar);

            Console.ReadLine();
        }

        static void LogCarDetails(Car car)
        {
            if (car is ICECar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICECarDetails.txt"), true))
                {
                    sw.WriteLine($"ObjectType: {car.GetType()}");
                    sw.WriteLine($"Car Details: {car.GetCarDetails()}");
                }
            }
            else if (car is EVCar)
            {
                Console.WriteLine($"ObjectType: {car.GetType()}");
                Console.WriteLine($"Car Details: {car.GetCarDetails()}");
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    public static class CarFactory
    {
        public static ICECar ReturnICECar(int id, string name)
        {
            return new ICECar { Id = id, Name = name };
        }
        public static EVCar ReturnEVCar(int id, string name)
        {
            return new EVCar { Id = id, Name = name };
        }
    }

    public abstract class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual string GetCarDetails()
        {
            return $"{Id} - {Name }";
        }

    }
    public class ICECar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Internal Combustion Engine";
        }
    }
    public class EVCar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Electric Vehicle";
        }
    }

}
