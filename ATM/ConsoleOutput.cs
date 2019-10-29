using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class ConsoleOutput : IOutput
    {
        public void Print(Plane plane)
        {
            if (plane.SeparationCond.Count > 0)
            {
                Console.WriteLine($"SEPARATION CONDITION ACTIVE ON ");
                for (int i = 0; i < plane.SeparationCond.Count; i++)
                {
                    Console.Write($" {plane.SeparationCond[i]}, ");
                }
                Console.Write($"at {plane.CurrentTime}");
            }
            Console.WriteLine($"Flight {plane.Tag}");
            Console.Write($"Position: ({plane.XCoordinate}, ");
            Console.Write($"{plane.YCoordinate}), ");
            Console.Write($"Altitude: {plane.ZCoordinate}, ");
            Console.Write($"Velocity: {plane.Velocity} m/s, ");
            Console.Write($"Bearing: {plane.Bearing} degrees ");
        }
    }
}
