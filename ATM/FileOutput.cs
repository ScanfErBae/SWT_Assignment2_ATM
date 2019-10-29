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
            content = plane.CurrentTime.ToLongDateString() + " " + plane.CurrentTime.ToLongTimeString() + ":" + plane.CurrentTime.Millisecond
             + "\n" + "Tag: " + plane.Tag + "\n";;
            string path = "";
            //path = @"C:\Users\Frederik\Documents\Uni\SWT\SWT_Assignment2_ATM\ATM\Output.txt";
            path =
                @"C:\Users\rasmu\OneDrive - Aarhus universitet\Skrivebord\4. Semester_m\SWT\ATM\SWT_Assignment2_ATM\ATM\Output.txt";
            //path = (Directory.GetCurrentDirectory() + @"\Output.txt");
            System.IO.File.AppendAllText(path, content);
        }
    }
}