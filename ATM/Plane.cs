using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ATM
{
    public class Plane : IPlane
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int ZCoordinate { get; set; }
        public DateTime CurrentTime { get; set; }

        public double Bearing { get; set; }

        public double Velocity { get; set; }
        public bool Relevant { get; set; }
        public List<string> SeparationCond { get; set; }

        private ICalculate _calculate;

        public Plane(string tag = "", int x = 0, int y = 0, int z = 0, DateTime t = new DateTime())
        {
            this._calculate = new Calculate();
            this.Tag = tag;
            this.XCoordinate = x;
            this.YCoordinate = y;
            this.ZCoordinate = z;
            this.CurrentTime = t;
            SeparationCond = new List<string>();
        }

        public Plane(Plane oldPlane)
        {
            Tag = oldPlane.Tag;
            XCoordinate = oldPlane.XCoordinate;
            YCoordinate = oldPlane.YCoordinate;
            ZCoordinate = oldPlane.ZCoordinate;
            CurrentTime = oldPlane.CurrentTime;
            Bearing = oldPlane.Bearing;
            Velocity = oldPlane.Velocity;
            Relevant = oldPlane.Relevant;
            SeparationCond = oldPlane.SeparationCond;
            _calculate = oldPlane._calculate;
        }

        public static bool operator ==(Plane obj1, Plane obj2)
        {
            return (obj1.Tag == obj2.Tag
                    && obj1.XCoordinate == obj2.XCoordinate
                    && obj1.YCoordinate == obj2.YCoordinate
                    && obj1.ZCoordinate == obj2.ZCoordinate);
        }

        public static bool operator !=(Plane obj1, Plane obj2)
        {
            return !(obj1.Tag == obj2.Tag
                     && obj1.XCoordinate == obj2.XCoordinate
                     && obj1.YCoordinate == obj2.YCoordinate
                     && obj1.ZCoordinate == obj2.ZCoordinate);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Plane p = (Plane)obj;
            return (Tag == p.Tag
                    && XCoordinate == p.XCoordinate
                    && YCoordinate == p.YCoordinate
                    && ZCoordinate == p.ZCoordinate);
        }

        public void UpdateData(int x, int y, int z, DateTime t)
        {
            Plane newPlane = new Plane(this.Tag, x, y, z, t);

            this.Bearing = _calculate.CalculateBearing(this, newPlane);

            this.Velocity = _calculate.CalculateVelocity(this, newPlane);
          
            this.XCoordinate = x;
            this.YCoordinate = y;
            this.ZCoordinate = z;
            this.CurrentTime = t;
            
        }


    }
}
