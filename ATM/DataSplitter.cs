using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class DataSplitter : IDataSplitter
    {
        private List<Plane> planeList = new List<Plane>();
        private ITransponderReceiver _receiver;
        public DataSplitter(ITransponderReceiver receiver)
        {
            this._receiver = receiver;
            this._receiver.TransponderDataReady += DataSplit;
        }
        public event EventHandler<AirplaneArgs> DataReceivedEvent;

        private void DataSplit(object sender, RawTransponderDataEventArgs e)
        {
            planeList.Clear();
            foreach (var data in e.TransponderData)
            {
                string[] input = data.Split(';');
                NewPlaneReceived(input);
            }
            OnDataReceivedEvent(new AirplaneArgs {_planes = planeList});
        }

        protected virtual void OnDataReceivedEvent(AirplaneArgs e)
        {
            DataReceivedEvent?.Invoke(this, e);
        }

        // Separates the received data into chunks that corresponds to the DataSplit class.
        // Time is separated into year, months, day, etc... 
        public void NewPlaneReceived(string [] data)
        {
            DateTime time = new DateTime(Int32.Parse(data[4].Substring(0,3)), Int32.Parse(data[4].Substring(4, 2)), 
                Int32.Parse(data[4].Substring(6, 2)), Int32.Parse(data[4].Substring(8, 2)), Int32.Parse(data[4].Substring(10, 2)),
                Int32.Parse(data[4].Substring(12, 2)), Int32.Parse(data[4].Substring(14, 3)));

            planeList.Add(new Plane(data[0], Int32.Parse(data[1]),Int32.Parse(data[2]),
                Int32.Parse(data[3]), time)
            );
        }
    }
}
