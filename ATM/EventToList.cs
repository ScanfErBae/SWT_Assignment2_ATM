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
        private ISeparationCondition _separationCondition;
        public EventToList(IFilter filter, ICalculate calculator, ISeparationCondition sepCond)
        {
            this._calculator = calculator;
            this._filter = filter;
            this._separationCondition = sepCond;
            this._filter.RelevantAirplanesReceivedEvent += ToList;
        }

        private void ToList(object sender, RelevantAirplaneArgs e)
        {
            foreach (Plane plane in _relevantPlanesList)
            {
                plane.Relevant = false;
            }
            Console.Clear();
            foreach (Plane newPlane in e._relevantPlanes)
            {
                bool test = false;
                if (_relevantPlanesList != null)
                {
                    foreach (Plane oldPlane in _relevantPlanesList)
                    {
                        if (newPlane.Tag == oldPlane.Tag)
                        {
                            oldPlane.Bearing = _calculator.CalculateBearing(oldPlane, newPlane);
                            oldPlane.Velocity = _calculator.CalculateVelocity(oldPlane, newPlane);
                            oldPlane.UpdateData(newPlane.XCoordinate, newPlane.YCoordinate, newPlane.ZCoordinate,
                                newPlane.CurrentTime);
                            test = true;
                            oldPlane.Relevant = true;
                            //Console.WriteLine($"Updated flight {oldPlane.Tag}");
                        }
                    }
                }
                if (!test)
                {
                    _relevantPlanesList.Add(new Plane(newPlane.Tag, newPlane.XCoordinate, newPlane.YCoordinate,
                        newPlane.ZCoordinate, newPlane.CurrentTime));
                    _relevantPlanesList.ElementAt(_relevantPlanesList.Count - 1).Relevant = true;
                    //Console.WriteLine($"Added flight {newPlane.Tag}");
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
                ////Console.WriteLine($"Removed flight {plane.Tag}");
                _relevantPlanesList.Remove(plane);
            }
            _separationCondition.Separation(_relevantPlanesList);
        }
    }
}
