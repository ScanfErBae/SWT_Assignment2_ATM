using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class SeparationCondition : ISeparationCondition
    {
        private IOutput _fileOutput;
        private IOutput _consoleOutput;
        public SeparationCondition(IOutput fileoutput, IOutput consoleOutput)
        {
            this._fileOutput = fileoutput;
            this._consoleOutput = consoleOutput;
        }
        private List<Plane> _oldSepPlanes = new List<Plane>();

        public bool ComparePlanes(Plane Air1, Plane Air2)
        {
            int Xdist = 0, Ydist = 0, Zdist = 0;

            Xdist = Math.Abs(Air1.XCoordinate - Air2.XCoordinate);
            Ydist = Math.Abs(Air1.YCoordinate - Air2.YCoordinate);
            Zdist = Math.Abs(Air1.ZCoordinate - Air2.ZCoordinate);

            if (Xdist < 300 && Ydist < 300 && Zdist < 5000)
            {
                return true;
            }
            return false;
        }

        public void Separation(List<Plane> planes)
        {
            int result = 0, i = 0, q = 0; 
            foreach (var Airplane1 in planes)
            {
                foreach (var Airplane2 in planes)
                {
                    if (q > i)
                    {
                        if (ComparePlanes(Airplane1, Airplane2) == true)
                        {
                            bool test = false;
                            foreach (var tag in Airplane1.SeparationCond)
                            {
                                if (tag == Airplane2.Tag)
                                {
                                    test = true; 
                                }
                            }

                            if (!test)
                            {
                                Airplane1.SeparationCond.Add(Airplane2.Tag);
                                Airplane2.SeparationCond.Add(Airplane1.Tag);
                                _fileOutput.Print(Airplane1);
                                _fileOutput.Print(Airplane2);
                            }
                        }
                        else
                        {
                            bool test2 = false;
                            foreach (var tag in Airplane1.SeparationCond)
                            {
                                if (tag == Airplane2.Tag)
                                {
                                    test2 = true;
                                }
                            }

                            if (test2)
                            {
                                Airplane1.SeparationCond.Remove(Airplane2.Tag);
                                Airplane2.SeparationCond.Remove(Airplane1.Tag);
                            }
                        }
                    }
                    q++;
                }
                i++;
                q = 0;
            }

            foreach (Plane plane in planes)
            {
                _consoleOutput.Print(plane);
            }
        }
    }

}
