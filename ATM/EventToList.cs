using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class EventToList
    {
        public List<Plane> _relevantPlanesList = new List<Plane>();
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
                            UpdatePlane(oldPlane, newPlane);
                            test = true;
                        }
                    }
                }
                if (!test)
                {
                    AddPlane(newPlane);
                }
            }

            RemoveOldPlanes(_relevantPlanesList);
            _separationCondition.Separation(_relevantPlanesList);
        }

        public void UpdatePlane(Plane oldPlane, Plane newPlane)
        {
            oldPlane.Relevant = true;
            _relevantPlanesList[_relevantPlanesList.IndexOf(oldPlane)].UpdateData(newPlane.XCoordinate, newPlane.YCoordinate, newPlane.ZCoordinate,
                newPlane.CurrentTime);
        }

        public void AddPlane(Plane newPlane)
        {
            _relevantPlanesList.Add(new Plane(newPlane.Tag, newPlane.XCoordinate, newPlane.YCoordinate,
                newPlane.ZCoordinate, newPlane.CurrentTime));
            _relevantPlanesList.ElementAt(_relevantPlanesList.Count - 1).Relevant = true;
        }

        public void RemoveOldPlanes(List<Plane> planeList)
        {
            List<Plane> planesToRemove = new List<Plane>();

            foreach (Plane plane in planeList)
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
        }
    }
}
