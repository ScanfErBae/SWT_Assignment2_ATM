using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderFilter
{
    public class Control
    {
        private string newRelevantPlane { get; set; }

        public Control(IRelevantPlane relevantPlane)
        {
            relevantPlane.RelevantPlaneEvent += HandleRelevantPlaneEvent;
        }

        private void HandleRelevantPlaneEvent(object sender, RelevantPlaneEventArgs e)
        {
            newRelevantPlane = e.Plane;
            System.Console.WriteLine($"Plane {newRelevantPlane}");
        }
    }

}
