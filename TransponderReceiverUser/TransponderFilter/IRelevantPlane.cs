using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderFilter
{
    public class RelevantPlaneEventArgs : EventArgs
    {
        public string Plane;
    }

    public interface IRelevantPlane 
    {
        event EventHandler<RelevantPlaneEventArgs> RelevantPlaneEvent;
        event EventHandler<RelevantPlaneEventArgs> NotRelevantPlaneEvent;
    }
    //public interface IIrrelevantPlane
    //{
    //    event EventHandler<RelevantPlaneEventArgs> IrrelevantPlaneEvent;
    //}
}
