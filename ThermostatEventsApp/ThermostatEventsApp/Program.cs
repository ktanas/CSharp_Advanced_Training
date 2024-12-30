using System;
using System.ComponentModel;

namespace ThermostatEventsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start device...");
            Console.ReadKey();

            IDevice device = new Device();

            device.RunDevice();

            Console.ReadLine();
        }
    }

    public class Device : IDevice
    {
        const double WarningLevel = 27;
        const double EmergencyLevel = 30;

        public double WarningTemperatureLevel => WarningLevel;

        public double EmergencyTemperatureLevel => EmergencyLevel;

        public void HandleEmergency()
        {
            Console.WriteLine();
            Console.WriteLine("Starting emergency routines...");
            ShutdownDevice();
            Console.WriteLine();
        }

        private void ShutdownDevice()
        {
            Console.WriteLine("Shutting down device...");
        }

        public void RunDevice()
        {
            Console.WriteLine("Device is running...");

            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeatSensor heatSensor = new HeatSensor(WarningLevel,EmergencyLevel);
            IThermostat thermostat = new Thermostat(this, heatSensor, coolingMechanism);

            thermostat.RunThermostat();
            heatSensor.RunHeatSensor();
        }
    }

    public class Thermostat : IThermostat
    {
        private ICoolingMechanism _coolingMechanism = null;
        private IHeatSensor _heatSensor = null;
        private IDevice _device = null;

        private const double WarningLevel = 27;
        private const double EmergencyLevel = 30;

        public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
        {
            _device = device;
            _heatSensor = heatSensor;
            _coolingMechanism = coolingMechanism;

        }

        private void WireUpEventsToEventHandlers()
        {
            _heatSensor.TemperatureReachesWarningLevelHandler += HeatSensor_TemperatureReachesWarningLevelHandler;
            _heatSensor.TemperatureReachesEmergencyLevelHandler += HeatSensor_TemperatureReachesEmergencyLevelHandler;
            _heatSensor.TemperatureFallsBelowWarningLevelHandler += HeatSensor_TemperatureFallsBelowWarningLevelHandler;
        }

        private void HeatSensor_TemperatureFallsBelowWarningLevelHandler(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Temperature has fallen safely below " + _device.WarningTemperatureLevel);
            _coolingMechanism.Off();
            Console.ResetColor();
        }

        private void HeatSensor_TemperatureReachesEmergencyLevelHandler(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("ALERT!!! Temperature has reached " + _device.EmergencyTemperatureLevel);
            _device.HandleEmergency();
            Console.ResetColor();
        }

        private void HeatSensor_TemperatureReachesWarningLevelHandler(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Warning! Temperature is between " + _device.WarningTemperatureLevel + " and " + _device.EmergencyTemperatureLevel);
            _coolingMechanism.On();
            Console.ResetColor();
        }

        public void RunThermostat()
        {
            Console.WriteLine("Thermostat is running...");
                WireUpEventsToEventHandlers();
        }
    }

    public interface IThermostat
    {
        void RunThermostat();
    }

    public interface IDevice
    {
        double WarningTemperatureLevel { get; }
        double EmergencyTemperatureLevel { get; }

        void RunDevice();
        void HandleEmergency();
    }

    public class CoolingMechanism : ICoolingMechanism
    {
        public void Off()
        {
            Console.WriteLine();
            Console.WriteLine("Switching cooling mechanism off...");
            Console.WriteLine();
        }

        public void On()
        {
            Console.WriteLine();
            Console.WriteLine("Switching cooling mechanism on...");
            Console.WriteLine();
        }
    }

    public interface ICoolingMechanism
    {
        void On();
        void Off();
    }

    public class HeatSensor : IHeatSensor
    {
        double _warningLevel = 0;
        double _emergencyLevel = 0;

        bool _hasReachedWarningTemperature = false;

        protected EventHandlerList _listEventDelegates = new EventHandlerList();

        static readonly object _temperatureReachesWarningLevelKey = new object();
        static readonly object _temperatureReachesEmergencyLevelKey = new object();
        static readonly object _temperatureFallsBelowWarningLevelKey = new object();

        private double[] _temperatureData = null;

        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _warningLevel = warningLevel;
            _emergencyLevel = emergencyLevel;

            SeedData();
        }

        protected void OnTemperatureReachesWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesWarningLevelKey];

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnTemperatureFallsBelowWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureFallsBelowWarningLevelKey];

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnTemperatureReachesEmergencyLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesEmergencyLevelKey];

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void MonitorTemperature()
        {
            foreach (double temperature in _temperatureData)
            {
                Console.ResetColor();
                Console.WriteLine($"DateTime: {DateTime.Now}, Temperature: {temperature}");

                if (temperature >= _emergencyLevel)
                {
                    TemperatureEventArgs e = new TemperatureEventArgs
                    {
                        Temperature = temperature,
                        CurrentDateTime = DateTime.Now
                    };
                    OnTemperatureReachesEmergencyLevel(e);
                }
                else if (temperature >= _warningLevel)
                {
                    _hasReachedWarningTemperature = true;

                    TemperatureEventArgs e = new TemperatureEventArgs
                    {
                        Temperature = temperature,
                        CurrentDateTime = DateTime.Now
                    };
                    OnTemperatureReachesWarningLevel(e);
                }
                else if (temperature < _warningLevel && _hasReachedWarningTemperature)
                {
                    _hasReachedWarningTemperature = false;

                    TemperatureEventArgs e = new TemperatureEventArgs
                    {
                        Temperature = temperature,
                        CurrentDateTime = DateTime.Now
                    };
                    OnTemperatureFallsBelowWarningLevel(e);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        private void SeedData()
        {
            _temperatureData = new double[] { 16, 17, 16.5, 20, 21.5, 19, 23, 24.75, 27,5, 30, 29, 26.17, 23.9, 30, 32 };
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesEmergencyLevelHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesEmergencyLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesEmergencyLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesWarningLevelHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);

            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureFallsBelowWarningLevelHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureFallsBelowWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureFallsBelowWarningLevelKey, value);
            }
        }

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat sensor is running...");
            MonitorTemperature();
        }
    }

    public interface IHeatSensor
    {
        event EventHandler<TemperatureEventArgs> TemperatureReachesEmergencyLevelHandler;
        event EventHandler<TemperatureEventArgs> TemperatureReachesWarningLevelHandler;
        event EventHandler<TemperatureEventArgs> TemperatureFallsBelowWarningLevelHandler;

        void RunHeatSensor();
    }

    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}
