using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class AirplaneArgs : EventArgs
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int ZCoordinate { get; set; }
        public string TimeYear { get; set; }
        public string TimeMonth { get; set; }
        public string TimeDay { get; set; }
        public string TimeHour { get; set; }
        public string TimeMinute { get; set; }
        public string TimeSecond { get; set; }
        public string TimeMilliSecond { get; set; }
    }
    interface IDataSplitter
    {
        event EventHandler<AirplaneArgs> DataReceivedEvent;
    }
}
