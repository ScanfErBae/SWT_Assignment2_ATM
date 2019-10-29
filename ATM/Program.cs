using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;


namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var datasplitter = new DataSplitter(receiver);
            var filter = new Filter(datasplitter);
            var calculator = new Calculate();
            var eventlist = new EventToList(filter, calculator);

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);
        }
    }
}
