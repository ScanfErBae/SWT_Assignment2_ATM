﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Calculate : ICalculate
    {
      public double CalculateVelocity(Plane oldPlane, Plane newPlane)
        {
            //Calculate time
            double yearCal = (newPlane.CurrentTime.Year - oldPlane.CurrentTime.Year) * 12 * 30 * 24 * 60 * 60 * 1000;
            double montCal = (newPlane.CurrentTime.Month - oldPlane.CurrentTime.Month) * 30 * 24 * 60 * 60 * 1000;
            double dayCal = (newPlane.CurrentTime.Day - oldPlane.CurrentTime.Day) * 24 * 60 * 60 * 1000;
            double hourCal = (newPlane.CurrentTime.Hour - oldPlane.CurrentTime.Hour) * 60 * 60 * 1000;
            double minCal = (newPlane.CurrentTime.Minute - oldPlane.CurrentTime.Minute) * 60 * 1000;
            double secCal = (newPlane.CurrentTime.Second - oldPlane.CurrentTime.Second) * 1000;
            double msCal = (newPlane.CurrentTime.Millisecond - oldPlane.CurrentTime.Millisecond);
            double totalTimeInSec = yearCal + montCal + dayCal + hourCal + minCal + secCal + msCal;

            //Calculate distance
            double distance = Math.Sqrt(Math.Pow((newPlane.XCoordinate - oldPlane.XCoordinate), 2) + Math.Pow((newPlane.YCoordinate - oldPlane.YCoordinate), 2));

            //Calculate Velocity
            return (distance / totalTimeInSec) * 1000;
        }

        public double CalculateBearing(Plane oldPlane, Plane newPlane)
        {
            double Rad2Deg = 180.0 / Math.PI;
            double Deg2Rad = Math.PI / 180.0;
            double dx = newPlane.XCoordinate - oldPlane.XCoordinate;
            double dy = newPlane.YCoordinate - oldPlane.YCoordinate;
            double Bearing = Math.Atan2(dy, dx) * Rad2Deg;
            if (Bearing < 0)
            {
                Bearing = Bearing + 360; 


            }
            return Bearing;
        }

        
        public double ToRad(double degrees)
        {
            if (degrees < 0 || degrees > 360)
            {
                return 0;
            }
            return degrees * (Math.PI / 180);
        }

        public double ToDegrees(double radians)
        {
            radians = (radians * 180 / Math.PI) % 360;
            if (radians < 0 || radians > 360)
            {
                return 0;
            }
            return radians;
        }

    }
}
