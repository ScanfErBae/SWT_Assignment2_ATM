using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionDetection
{
    public class Plane : IPlane
    {
        private PlaneState _planeState;
        private int _oldX;
        private int _oldY;
        private int _oldZ;
        private string _tag;
        public event EventHandler<AirplaneArgs> NewAirPlanesEvent;

        public Plane()
        {
            _planeState = new PlaneStateSafe();
        }
        public void SetCoordinates(int newX, int newY, int newZ)
        {
            if (newX != _oldX || newY != _oldY || newZ != _oldZ)
            {
                OnCoordsChangedEvent(new AirplaneArgs {XCoordinate = newX, YCoordinate = newY, ZCoordinate = newZ});
                _oldX = newX;
                _oldY = newY;
                _oldZ = newZ;
                _planeState.HandleNewPlaneState(this);
            }

        }

        protected virtual void OnCoordsChangedEvent(AirplaneArgs e)
        {
            NewAirPlanesEvent?.Invoke(this, e);
        }

        public void SetState(PlaneState s)
        {
            _planeState = s;
        }
    }
}
