using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class EventToList
    {
        private List<Plane> _relevantPlanesList = new List<Plane>();
        private IFilter _filter;
        private ICalculate _calculator;
        public EventToList(IFilter filter, ICalculate calculator)
        {
            this._calculator = calculator;
            this._filter = filter;
            this._filter.RelevantAirplanesReceivedEvent += ToList;
        }

        private void ToList(object sender, RelevantAirplaneArgs e)
        {
            foreach (Plane plane in _relevantPlanesList)
            {
                plane.Relevant = false;
            }

            foreach (Plane newPlane in e._relevantPlanes)
            {
                bool test = false;
                if (_relevantPlanesList != null)
                {
                    foreach (Plane oldPlane in _relevantPlanesList)
                    {
                        if (newPlane.Tag == oldPlane.Tag)
                        {
                            oldPlane.UpdateData(newPlane.XCoordinate, newPlane.YCoordinate, newPlane.ZCoordinate,
                                newPlane.CurrentTime);
                            test = true;
                            oldPlane.Relevant = true;
                        }
                    }

                    if (!test)
                    {
                        _relevantPlanesList.Add(new Plane(newPlane.Tag, newPlane.XCoordinate, newPlane.YCoordinate,
                            newPlane.ZCoordinate, newPlane.CurrentTime));
                        _relevantPlanesList.ElementAt(_relevantPlanesList.Count - 1).Relevant = true;
                    }
                }
            }

            List<Plane> planesToRemove = new List<Plane>();

            foreach (Plane plane in _relevantPlanesList)
            {
                if (!plane.Relevant)
                {
                    planesToRemove.Add(plane);
                }
            }

            foreach (Plane plane in planesToRemove)
            {
                _relevantPlanesList.Remove(plane);
            }


        }
    }
}
