using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class ConsoleOutput : IOutput
    {
        public string planeTag { get; set; }
        public string planePositionX { get; set; }
        public string planePositionY { get; set; }
        public string planeAltitude { get; set; }
        public string planeVelocity { get; set; }
        public string planeBearing { get; set; }
        public string planeCondInfo { get; set; }


        public ConsoleOutput(string tag = "", string x = "", string y = "", string a = "", string v = "", string b = "", string cond = "")
        {
            planeTag = tag;
            planePositionX = x;
            planePositionY = y;
            planeAltitude = a;
            planeVelocity = v;
            planeBearing = b;
            planeCondInfo = cond;
        }

        public void Print(Plane plane1)
        {


            ConsoleWrite output = new ConsoleWrite();
            planeCondInfo = "";

            Plane plane = new Plane(plane1);
            if (plane.SeparationCond.Count > 0)
            {
                planeCondInfo = ($"SEPARATION CONDITION ACTIVE ON: Flight {plane.Tag} in connection with");
                for (int i = 0; i < plane.SeparationCond.Count; i++)
                {
                    planeCondInfo += ($" {plane.SeparationCond[i]}, ");
                }
                planeCondInfo += ($"at {plane.CurrentTime}\n");
                output.ConsoleWriteCondition(planeCondInfo);
            }
            string result1 = string.Format("{0:0.00}", plane.Velocity);
            string result2 = string.Format("{0:0.00}", plane.Bearing);

            planeTag = ($"Flight {plane.Tag} \t");
            planePositionX = ($"Position: ({plane.XCoordinate}, ");
            planePositionY = ($"{plane.YCoordinate}) \t ");
            planeAltitude = ($"Altitude: {plane.ZCoordinate}   \t");
            planeVelocity = ($"Velocity: {result1} m/s \t");
            planeBearing = ($"Bearing: {result2} degrees \n");
            output.ConsoleWritePlane(planeTag,planePositionX,planePositionY,planeAltitude,planeVelocity,planeBearing);
        }
    }
}