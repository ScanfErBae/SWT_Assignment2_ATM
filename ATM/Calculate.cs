using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 

namespace ATM
{
    class Calculate : ICalculate
    {
      public double CalculateVelocity(int x1, int y1, int year1, int month1, int day1, int hour1, int min1, int sec1, int ms1, int x2, int y2, int year2, int month2, int day2, int hour2, int min2, int sec2, int ms2)
        {
            //Calculate distance
            double distance = 0;
            distance = Math.Sqrt(((x2 - x1) ^ 2) + ((y2 - y1) ^ 2));
            return 20;
        }

        public double CalculateBearing(int lat1, int lon1, int lat2, int lon2)
        {
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        public double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public double ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }
    }
}
