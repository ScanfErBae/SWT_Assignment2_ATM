using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvent
{
    public class TempSensor : ITempSensor
    {
        public event EventHandler<TempChangedEventArgs> TempChangedEvent;

        private int _oldTemp = -100;  // Initialize to improbable temperature

        public void SetTemp(int newTemp)
        {
            if (newTemp != _oldTemp)
            {
                OnTempChangedEvent(new TempChangedEventArgs { Temp = newTemp });
                _oldTemp = newTemp;
            }
        }

        protected virtual void OnTempChangedEvent(TempChangedEventArgs e)
        {
            TempChangedEvent?.Invoke(this, e);
        }

        private Random gen = new Random();

        public bool RunSelfTest()
        {
            // 5 % chance of failing
            return gen.Next(0,100) < 5 ? false : true;
        }
    }
}
