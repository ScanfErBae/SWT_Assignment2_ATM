using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    interface IPlane
    {
        Plane UpdateData(int x, int y, int z, DateTime t);

        public Plane(Plane oldPlane);
    }
}
