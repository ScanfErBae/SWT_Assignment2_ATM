using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    interface IPlane
    {
        void UpdateData(int X, int Y, int Z, int year, int month, int day, int hour, int minute, int sec, int ms);

    }
}
