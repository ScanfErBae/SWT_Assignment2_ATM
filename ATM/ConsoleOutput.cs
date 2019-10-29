using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class ConsoleOutput : IOutput
    {
        public void Print(Plane plane1)
        {
            Plane plane = new Plane(plane1);

            if (plane.SeparationCond.Count > 0)
            {
                Console.Write($"SEPARATION CONDITION ACTIVE ON ");
                for (int i = 0; i < plane.SeparationCond.Count; i++)
                {
                    Console.Write($" {plane.SeparationCond[i]}, ");
                }
                Console.Write($"at {plane.CurrentTime}\n");
            }
            Console.Write($"Flight {plane.Tag} \t");
            Console.Write($"Position: ({plane.XCoordinate}, ");
            Console.Write($"{plane.YCoordinate}) \t ");
            Console.Write($"Altitude: {plane.ZCoordinate}   \t");
            string result = string.Format("{0:0.00}", plane.Velocity);
            Console.Write($"Velocity: {result} m/s \t");
            result = string.Format("{0:0.00}", plane.Bearing);
            Console.Write($"Bearing: {result} degrees \n");
        }
    }
}