using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TransponderReceiver;


namespace ATM
{
    public class FileOutput
    {
        public void Print(Plane plane)
        {
            string content = "";
            content = plane.CurrentTime.ToLongDateString() + " " + plane.CurrentTime.ToLongTimeString() + ":" + plane.CurrentTime.Millisecond
             + "\n" + "Tag: " + plane.Tag + "\n";;
            string path = "";
            //path = @"C:\Users\Frederik\Documents\Uni\SWT\SWT_Assignment2_ATM\ATM\Output.txt";
            path = (Directory.GetCurrentDirectory() + @"\Output.txt");
            System.IO.File.AppendAllText(path, content);
        }
    }
}