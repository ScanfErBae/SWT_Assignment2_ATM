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
        private List<AirplaneArgs> planeList;
        private ITransponderReceiver _receiver;
        public DataSplitter(ITransponderReceiver receiver)
        {
            this._receiver = receiver;
            this._receiver.TransponderDataReady += DataSplit;
        }
        public event EventHandler<List<AirplaneArgs>> DataReceivedEvent;

        private void DataSplit(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                string[] input = data.Split(';');
                NewPlaneReceived(input);
            }
            OnDataReceivedEvent(planeList);
        }

        protected virtual void OnDataReceivedEvent(List<AirplaneArgs> e)
        {
            DataReceivedEvent?.Invoke(this, e);
        }

        // Separates the received data into chunks that corresponds to the DataSplit class.
        // Time is separated into year, months, day, etc... 
        public void NewPlaneReceived(string [] data)
        {
            planeList.Add(new AirplaneArgs
            {
                Tag = data[0], XCoordinate = Int32.Parse(data[1]), YCoordinate = Int32.Parse(data[2]),
                ZCoordinate = Int32.Parse(data[3]), TimeYear = Int32.Parse(data[4].Substring(0,3)), 
                TimeMonth = Int32.Parse(data[4].Substring(4, 5)), TimeDay = Int32.Parse(data[4].Substring(6, 7)), 
                TimeHour = Int32.Parse(data[4].Substring(8, 9)), TimeMinute = Int32.Parse(data[4].Substring(10, 11)), 
                TimeSecond = Int32.Parse(data[4].Substring(12, 13)), TimeMillisecond = Int32.Parse(data[4].Substring(14, 16))
            });
        }
    }
}
