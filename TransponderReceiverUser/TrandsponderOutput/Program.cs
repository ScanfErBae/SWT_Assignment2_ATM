using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Dette er en test\n";

            string path =
                @"C:\SWT\AfleveringATM\SWT_Assignment2_ATM\TransponderReceiverUser\TrandsponderOutput\OutputFile\Output.txt";

            FileOutput test = new FileOutput();

            test.Print(path, text);
        }
    }
}