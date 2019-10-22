using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConditionDetection;

namespace TransponderFilter
{
    public class Control
    {
        private List<PlaneClass> _planes;
        private string newRelevantPlane { get; set; }
        private string NotRelevantPlane { get; set; }

        public Control(IRelevantPlane relevantPlane)
        {
            _planes = new List<PlaneClass>();
            relevantPlane.RelevantPlaneEvent += HandleRelevantPlaneEvent;
            relevantPlane.NotRelevantPlaneEvent += HandleIrrelevantPlaneEvent;

        }

        private void HandleIrrelevantPlaneEvent(object sender, RelevantPlaneEventArgs e)
        {
            PlaneClass PlaneToRemove = null;
            NotRelevantPlane = e.Plane;
            string[] input = NotRelevantPlane.Split(';');
            lock(_planes)
            { 
            foreach (PlaneClass plane in _planes)
                {
                    if (input[0] == plane._tag)
                    {
                        PlaneToRemove = plane;
                        System.Console.WriteLine("*****************************************");
                    }
                }
            if(PlaneToRemove!=null)
                _planes.Remove(PlaneToRemove);
            }
        }

        



        
       
        private void HandleRelevantPlaneEvent(object sender, RelevantPlaneEventArgs e)
        {
            newRelevantPlane = e.Plane;
            
            bool test = false;
            string[] input = newRelevantPlane.Split(';');
            
            if (_planes != null)
            {

                foreach (PlaneClass plane in _planes)
                {
                    if (input[0] == plane._tag)
                    {
                        plane.SetCoordinates(Int32.Parse(input[1]), Int32.Parse(input[2]), Int32.Parse(input[3]),
                            input[4]);
                        System.Console.WriteLine($"Updated{input[0]}");
                        test = true;
                    }
                }
            }
            lock (_planes)
            { 
                if (!test)
                {
                    _planes.Add(new PlaneClass(input[0], Int32.Parse(input[1]), Int32.Parse(input[2]), Int32.Parse(input[3]), input[4]));
                    System.Console.WriteLine($"Added{input[0]}");
                }
            }



        }
    }

}
