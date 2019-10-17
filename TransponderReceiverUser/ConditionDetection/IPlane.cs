using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionDetection
{
    public class AirplaneArgs : EventArgs
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int ZCoordinate { get; set; }
        public string Tag { get; set; }

    }
    public interface IPlane
    {
        event EventHandler<AirplaneArgs> NewAirPlanesEvent;
    }
}
