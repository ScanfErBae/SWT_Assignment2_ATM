using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class RelevantAirplaneArgs : EventArgs
    {
        public List<Plane> _relevantPlanes { get; set; }
    }

    interface IFilter
    {
        event EventHandler<RelevantAirplaneArgs> RelevantAirplanesReceivedEvent;
    }
}
