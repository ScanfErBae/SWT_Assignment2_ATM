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
        public int TimeYear { get; set; }
        public int TimeMonth { get; set; }
        public int TimeDay { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public int TimeSecond { get; set; }
        public int TimeMillisecond { get; set;}
    }
    interface IDataSplitter
    {
        event EventHandler<List<AirplaneArgs>> DataReceivedEvent;
    }
}
