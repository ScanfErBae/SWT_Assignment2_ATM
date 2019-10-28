using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class AirplaneArgs : EventArgs
    {
        public List<Plane> _planes { get; set; }
    }
    public interface IDataSplitter
    {
        event EventHandler<AirplaneArgs> DataReceivedEvent;
    }
}
