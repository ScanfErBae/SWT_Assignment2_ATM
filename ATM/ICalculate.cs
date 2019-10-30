using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public interface ICalculate
    {
        double CalculateVelocity(Plane oldPlane, Plane newPlane);
        double CalculateBearing(Plane oldPlane, Plane newPlane);
    }
}
