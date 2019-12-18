using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TransponderReceiver;


namespace ATM
{
    public class FileOutput : IOutput
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Output.txt";

        // Output filen ender på dit skrivebord!!!

        public void Print(Plane plane)
        {
            string content = "";
            content = plane.CurrentTime.ToLongDateString() + " Kl: " + plane.CurrentTime.Hour + ":" + plane.CurrentTime.Minute + ":" + plane.CurrentTime.Second + ":" + plane.CurrentTime.Millisecond + " Plane: " + plane.Tag + " Close to: " + plane.SeparationCond[0] + "\n";
            System.IO.File.AppendAllText(path, "Hej");
        }
    }
}