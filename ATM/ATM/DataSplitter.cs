using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    class DataSplitter : IDataSplitter
    {
        private ITransponderReceiver _receiver;
        public DataSplitter(ITransponderReceiver receiver)
        {
            this._receiver = receiver;
            this._receiver.TransponderDataReady += DataSplit;
        }
        public event EventHandler<AirplaneArgs> DataReceivedEvent;

        private void DataSplit(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                string[] input = data.Split(';');
                NewPlaneReceived(input);
            }
        }

        protected virtual void OnDataReceivedEvent(AirplaneArgs e)
        {
            DataReceivedEvent?.Invoke(this, e);
        }

        public void NewPlaneReceived(string [] data)
        {
            OnDataReceivedEvent(new AirplaneArgs
            {
                Tag = data[0], XCoordinate = Int32.Parse(data[1]), YCoordinate = Int32.Parse(data[2]),
                ZCoordinate = Int32.Parse(data[3]), TimeYear = data[4].Substring(0,3), 
                TimeMonth = data[4].Substring(4, 5), TimeDay = data[4].Substring(6, 7), 
                TimeHour = data[4].Substring(8, 9), TimeMinute = data[4].Substring(10, 11), 
                TimeSecond = data[4].Substring(12, 13), TimeMilliSecond = data[4].Substring(14, 16)
            });
        }
    }
}
