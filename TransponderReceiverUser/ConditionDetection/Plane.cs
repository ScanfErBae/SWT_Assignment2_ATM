using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionDetection
{
    public class PlaneClass : IPlane
    {
        private PlaneState _planeState;
        private int _oldX;
        private int _oldY;
        private int _oldZ;
        public string _tag { private set; get; }
        private string _timeStamp;
        private int _velocity;
        private int _course;
        public event EventHandler<AirplaneArgs> NewAirPlanesEvent;

        public PlaneClass(string tag, int x, int y, int z, string time)
        {
            _tag = tag;
            _oldX = x;
            _oldY = y;
            _oldZ = z;
            _timeStamp = time;
          //  _planeState = new PlaneStateSafe();
        }
        public void SetCoordinates(int newX, int newY, int newZ, string newTime)
        {
            if (newX != _oldX || newY != _oldY || newZ != _oldZ)
            {
                OnCoordsChangedEvent(new AirplaneArgs {XCoordinate = newX, YCoordinate = newY, ZCoordinate = newZ, TimeStamp = newTime});
                _oldX = newX;
                _oldY = newY;
                _oldZ = newZ;
                _timeStamp = newTime;
               // _planeState.HandleNewPlaneState(this);
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
