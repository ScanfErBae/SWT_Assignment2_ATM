using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    interface ICalculate
    {
        double CalculateBearing(int lat1, int lon1, int lat2, int lon2);
        double CalculateVelocity(int x1, int y1, int year1, int month1, int day1, int hour1, int min1, int sec1, int ms1, int x2, int y2, int year2, int month2, int day2, int hour2, int min2, int sec2, int ms2);



    }
}
