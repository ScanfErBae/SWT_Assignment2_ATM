using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Filter : IFilter
    {
        private IDataSplitter _dataSplitter;
        private List<Plane> RelevantPlanes;


        public Filter(IDataSplitter dataSplitter)
        {
            this._dataSplitter = dataSplitter;
            this._dataSplitter.DataReceivedEvent += FilterInput;
            RelevantPlanes = new List<Plane>();
        }

        private void FilterInput(object sender, List<AirplaneArgs> e)
        {
            foreach (var plane in e)
            {
                bool test = false;
                if (10000 <= plane.XCoordinate && plane.XCoordinate <= 90000 && 10000 <= plane.YCoordinate && plane.YCoordinate <= 90000 &&
                    500 <= plane.ZCoordinate && plane.ZCoordinate <= 20000)
                {
                    if (RelevantPlanes != null)
                    {
                        foreach (var Plane in RelevantPlanes)
                        {
                            if (plane.Tag == Plane.Tag)
                            {
                                Plane.UpdateData(plane.XCoordinate, plane.YCoordinate, plane.ZCoordinate, plane.TimeYear, plane.TimeMonth, plane.TimeDay, plane.TimeHour, plane.TimeMinute, plane.TimeSecond, plane.TimeMillisecond);
                                test = true;
                            }

                        }
                    }

                    if (!test)
                    {
                        if (RelevantPlanes != null)
                            RelevantPlanes.Add(new Plane(new Calculate(), plane.Tag, plane.XCoordinate, plane.YCoordinate,
                                plane.ZCoordinate, plane.TimeYear, plane.TimeMonth, plane.TimeDay, plane.TimeHour, plane.TimeMinute, plane.TimeSecond,
                                plane.TimeMillisecond));
                    }

                }


                else
                {
                    {
                        Plane toRemove = null;
                        foreach (var Airplane in RelevantPlanes)
                        {
                            if (plane.Tag == Airplane.Tag)
                                toRemove = Airplane;
                        }

                        if (toRemove != null)
                        {
                            RelevantPlanes.Remove(toRemove);
                        }
                    }

                }

            }
        }
    }
}
