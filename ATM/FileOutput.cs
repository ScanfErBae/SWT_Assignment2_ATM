﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TransponderReceiver;


namespace ATM
{
    public class FileOutput : IOutput
    {
        public void Print(Plane plane)
        {
            string content = "";
            content = plane.CurrentTime.ToLongDateString() + " " + plane.CurrentTime.ToLongTimeString() + ":" + plane.CurrentTime.Millisecond + "Plane: " + plane.Tag + " Close to: " + plane.SeparationCond;;
            string path = "";
            //path = @"C:\Users\Frederik\Documents\Uni\SWT\SWT_Assignment2_ATM\ATM\Output.txt";
            path = (Directory.GetCurrentDirectory() + @"\Output.txt");
            System.IO.File.AppendAllText(path, content);
        }
    }
}