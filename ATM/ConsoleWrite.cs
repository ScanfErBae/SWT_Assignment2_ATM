using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class ConsoleWrite: IConsoleWrite
    {
      public void ConsoleWritePlane(string PlaneTag, string PositionX, string PositionY, string Altitude, string Velocity,
            string Bearing)
        {
            Console.Write(PlaneTag + PositionX + PositionY + Altitude + Velocity + Bearing);
        }

        public void ConsoleWriteCondition(string Conditions)
        {
            Console.Write(Conditions);
        }
    }
}
