using System;
using System.Collections.Generic;
using System.Text;
using TransponderReceiver;


namespace ATM
{
    public class FileOutput
    {

        private ITransponderReceiver receiver;

        public FileOutput(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }


        public void Print(string contents)
        {
            string path =
                @"C:\SWT\AfleveringATM\SWT_Assignment2_ATM\TransponderReceiverUser\TrandsponderOutput\OutputFile\Output.txt";
            System.IO.File.AppendAllText(path, contents);
        }


        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {
                //System.Console.WriteLine($"Transponderdata {data}");
                string[] input = data.Split(';');
                if (10000 <= Int32.Parse(input[1]) && Int32.Parse(input[1]) <= 90000 &&
                    10000 <= Int32.Parse(input[2]) && Int32.Parse(input[2]) <= 90000 && 500 <= Int32.Parse(input[3]) &&
                    Int32.Parse(input[3]) <= 20000)
                {
                    Print($"Transponderdata{data}\n");
                }
                else
                {
                    Print("Irrelevant fly\n");
                }

            }
        }
    }
}