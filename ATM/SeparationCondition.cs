using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class SeparationCondition : ISeparationCondition
    {
        public void separation(List<Plane> planes)
        {
            int result = 0, i = 0, q = 1;
            foreach (var Airplane in planes)
            {
                foreach (var Airplane2 in planes)
                {
                    if (q>i)
                    {
                        if (ComparePlanes(Airplane, Airplane2) == true)
                        {

                        }
                        else
                        {
                            
                        }
                    }
                    q++;
                }
                i++;
            }
        }

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

        public void Separation(int value)
        {
            throw new NotImplementedException();
        }
    }

}
