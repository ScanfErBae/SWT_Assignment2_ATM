using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionDetection
{
    public abstract class PlaneState
    {
        public abstract void OnEnter();
        public abstract void HandleNewPlaneState(IPlane plane);
    }
}
