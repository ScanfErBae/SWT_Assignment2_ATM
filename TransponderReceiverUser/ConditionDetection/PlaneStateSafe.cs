using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionDetection
{
    class PlaneStateSafe : PlaneState
    {
        public override void OnEnter()
        {
            throw new NotImplementedException();
        }

        public override void HandleNewPlaneState(IPlane plane)
        {
            throw new NotImplementedException();
        }
    }
}
