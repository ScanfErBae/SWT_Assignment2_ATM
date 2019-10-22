using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ConditionDetection;


namespace TransponderFilter
{
    public class FilterRelevantPlanes : IRelevantPlane
    {
        private ITransponderReceiver receiver;
        public int RelevantPlanes;
        public int NotRelevantPlanes;
      
        
        public FilterRelevantPlanes(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }


      
        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            RelevantPlanes = 0;
            NotRelevantPlanes = 0;
            // Just display data
            foreach (var data in e.TransponderData)
            {
                //System.Console.WriteLine($"Transponderdata {data}");
                string[] input = data.Split(';');

                if (10000 <= Int32.Parse(input[1]) && Int32.Parse(input[1]) <= 90000 &&
                    10000 <= Int32.Parse(input[2]) && Int32.Parse(input[2]) <= 90000 && 500 <= Int32.Parse(input[3]) &&
                    Int32.Parse(input[3]) <= 20000)
                    {
                        //System.Console.WriteLine($"Transponderdata {data}");
                        NewRelevantPlane(data);
                        ++RelevantPlanes;
                        //System.Console.WriteLine("relevant fly");

                    }

                else
                {
                    NewNotRelevantPlane(data);
                    ++NotRelevantPlanes;
                    //System.Console.WriteLine("NotRelevant fly");
                }
                
            }
        }

        public event EventHandler<RelevantPlaneEventArgs> RelevantPlaneEvent;
        public event EventHandler<RelevantPlaneEventArgs> NotRelevantPlaneEvent;

        protected virtual void OnRelevantPlaneEvent(RelevantPlaneEventArgs e)
        {
            RelevantPlaneEvent?.Invoke(this, e);
        }

        protected virtual void OnNotRelevantPlaneEvent(RelevantPlaneEventArgs e)
        {
            NotRelevantPlaneEvent?.Invoke(this, e);
        }
        public void NewRelevantPlane(string NewPlane)
        {
            OnRelevantPlaneEvent(new RelevantPlaneEventArgs { Plane = NewPlane });
        }

        public void NewNotRelevantPlane(string NewPlane)
        {
            OnNotRelevantPlaneEvent(new RelevantPlaneEventArgs { Plane = NewPlane });
        }
    }



}
