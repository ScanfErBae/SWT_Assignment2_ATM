using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvent
{
    public class ECS
    {
        private readonly ITempSensor _tempSensor;
        private readonly IHeater _heater;
        private readonly IWindow _window;
        private int _lowerTemperatureThreshold;
        private int _upperTemperatureThreshold;

        public int CurrentTemperature { private set; get; }

        // Property for heating threshold
        public int LowerTemperatureThreshold
        {
            get { return _lowerTemperatureThreshold; }
            set
            {
                // Validation is done in the property set method
                // value is the built in name for the set value
                if (value <= _upperTemperatureThreshold)
                    _lowerTemperatureThreshold = value;
                else throw new ArgumentException("Lower threshold must be <= upper threshold");
            }
        }


        // Property for window threshold
        public int UpperTemperatureThreshold
        {
            get { return _upperTemperatureThreshold; }
            set
            {
                // Validation is done in the property set method
                // value is the built in name for the set value
                if (value >= _lowerTemperatureThreshold)
                    _upperTemperatureThreshold = value;
                else throw new ArgumentException("Upper threshold must be <= lower threshold");
            }
        }

        // We use constructor injection for all dependencies
        public ECS(ITempSensor tempSensor, IHeater heater, IWindow window, int lowerTemperatureThreshold, int upperTemperatureThreshold)
        {
            // Save references to dependencies
            _tempSensor = tempSensor;
            _heater = heater;
            _window = window;

            // Initialize properties
            UpperTemperatureThreshold = upperTemperatureThreshold;
            LowerTemperatureThreshold = lowerTemperatureThreshold;

            _tempSensor.TempChangedEvent += HandleTempChangedEvent;
        }

        private void HandleTempChangedEvent(object sender, TempChangedEventArgs e)
        {
            CurrentTemperature = e.Temp;
            Regulate();
        }

        public void Regulate()
        {
            // Determine which action to take according to the temperature
            if (CurrentTemperature < LowerTemperatureThreshold)
            {
                _heater.TurnOn();
                _window.Close();
            }
            else if (CurrentTemperature >= LowerTemperatureThreshold && CurrentTemperature <= UpperTemperatureThreshold)
            {
                _heater.TurnOff();
                _window.Close();
            }
            else
            {
                _heater.TurnOff();
                _window.Open();
            }
        }

        public bool RunSelfTest()
        {
            return 
                _tempSensor.RunSelfTest() 
                && 
                _heater.RunSelfTest()
                &&
                _window.RunSelfTest();
        }

    }
}
