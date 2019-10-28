using System;
using System.Collections.Generic;
using System.IO;
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
            string path = "";

            path = (Directory.GetCurrentDirectory() + @"\Output.txt");
            System.IO.File.AppendAllText(path, contents);
        }
        
    }
}