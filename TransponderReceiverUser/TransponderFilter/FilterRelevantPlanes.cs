using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace TransponderFilter
{
    public class FilterRelevantPlanes
    {
        private ITransponderReceiver receiver;

        public FilterRelevantPlanes(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
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
                    System.Console.WriteLine($"Transponderdata {data}");
                }
                else
                {
                    System.Console.WriteLine("Irrelevant fly");
                }
                
            }
        }
    }
}
