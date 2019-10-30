using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    interface IConsoleWrite
    {
        void ConsoleWritePlane(string PlaneTag, string PositionX, string PositionY, string Altitude, string Velocity, string Bearing);

        void ConsoleWriteCondition(string Condition);
    }
}
