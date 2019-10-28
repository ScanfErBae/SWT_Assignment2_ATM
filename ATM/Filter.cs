using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Filter : IFilter
    {
        private List<Plane> RelevantPlanes = new List<Plane>();
        private IDataSplitter _dataSplitter;


        public Filter(IDataSplitter dataSplitter)
        {
            this._dataSplitter = dataSplitter;
            this._dataSplitter.DataReceivedEvent += FilterInput;
        }

        public event EventHandler<RelevantAirplaneArgs> RelevantAirplanesReceivedEvent;

        protected virtual void OnRelevantAirplanesReceivedEvent(RelevantAirplaneArgs e)
        {
            RelevantAirplanesReceivedEvent?.Invoke(this, e);
        }

        private void FilterInput(object sender, AirplaneArgs e)
        {
            RelevantPlanes.Clear();

            foreach (Plane NewPlane in e._planes)
            {
                if (10000 <= NewPlane.XCoordinate && NewPlane.XCoordinate <= 90000 && 10000 <= NewPlane.YCoordinate && NewPlane.YCoordinate <= 90000 && 
                    500 <= NewPlane.ZCoordinate && NewPlane.ZCoordinate <= 20000)
                    if (RelevantPlanes != null)
                    {
                        NewRelevantPlaneReceived(NewPlane);
                    }
            }

            OnRelevantAirplanesReceivedEvent(new RelevantAirplaneArgs{_relevantPlanes = RelevantPlanes });
        }
        public void NewRelevantPlaneReceived(Plane NewRelevantPlane)
        {
            RelevantPlanes.Add(new Plane(NewRelevantPlane.Tag, NewRelevantPlane.XCoordinate,
                NewRelevantPlane.YCoordinate, NewRelevantPlane.ZCoordinate, NewRelevantPlane.CurrentTime));
        }

    }
}
