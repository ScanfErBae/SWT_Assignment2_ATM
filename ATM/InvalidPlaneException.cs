using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class InvalidPlaneException : Exception
    {
        public InvalidPlaneException(string s) : base(s)
        {

        }
    }
}
