using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class ConsoleOutput : IOutput
    {
        public void Print(Plane plane1)
        {
            ConsoleWrite output = new ConsoleWrite();
            string planeCondInfo = "";

            Plane plane = new Plane(plane1);

            if (plane.SeparationCond.Count > 0)
            {
                planeCondInfo = ($"SEPARATION CONDITION ACTIVE ON ");
                for (int i = 0; i < plane.SeparationCond.Count; i++)
                {
                    planeCondInfo += ($" {plane.SeparationCond[i]}, ");
                }
                planeCondInfo += ($"at {plane.CurrentTime}\n");
                output.ConsoleWriteCondition(planeCondInfo);
            }
            string result1 = string.Format("{0:0.00}", plane.Velocity);
            string result2 = string.Format("{0:0.00}", plane.Bearing);

            string planeTag = ($"Flight {plane.Tag} \t");
            string planePositionX = ($"Position: ({plane.XCoordinate}, ");
            string planePositionY = ($"{plane.YCoordinate}) \t ");
            string planeAltitude = ($"Altitude: {plane.ZCoordinate}   \t");
            string planeVelocity = ($"Velocity: {result1} m/s \t");
            string planeBearing = ($"Bearing: {result2} degrees \n");
            output.ConsoleWritePlane(planeTag,planePositionX,planePositionY,planeAltitude,planeVelocity,planeBearing);
        }
    }
}