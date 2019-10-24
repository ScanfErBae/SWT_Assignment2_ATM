using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ATM
{
    class Plane : IPlane
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int ZCoordinate { get; set; }
        public int TimeYear { get; set; }
        public int TimeMonth { get; set; }
        public int TimeDay { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public int TimeSecond { get; set; }
        public int TimeMillisecond { get; set; }

        public double Bearing { get; set; }

        public double Velocity { get; set; }
        public bool Relevant { get; set; }

        private ICalculate _calculate;

        public Plane(string tag, int X, int Y, int Z, int year, int month, int day, int hour,
            int minute, int sec, int ms)
        {
            //this._calculate = calculate;
            this.XCoordinate = X;
            this.YCoordinate = Y;
            this.ZCoordinate = Z;
            this.TimeYear = year;
            this.TimeMonth = month;
            this.TimeDay = day;
            this.TimeHour = hour;
            this.TimeMinute = minute;
            this.TimeSecond = sec;
            this.TimeMillisecond = ms;
        }

        public void UpdateData(int X, int Y, int Z, int year, int month, int day, int hour, int minute, int sec, int ms )
        {
            this.Bearing = _calculate.CalculateBearing(this.XCoordinate, this.YCoordinate, X, Y);

            this.Velocity = _calculate.CalculateVelocity(this.XCoordinate, this.YCoordinate, this.TimeYear, this.TimeMonth, this.TimeDay, this.TimeHour, this.TimeMinute, this.TimeSecond, this.TimeMillisecond,X, Y, year, month, day, hour, minute, sec, ms);
          
            this.XCoordinate = X;
            this.YCoordinate = Y;
            this.ZCoordinate = Z;
            this.TimeYear = year;
            this.TimeMonth = month;
            this.TimeDay = day;
            this.TimeHour = hour;
            this.TimeMinute = minute;
            this.TimeSecond = sec;
            this.TimeMillisecond = ms;
            
        }


    }
}
