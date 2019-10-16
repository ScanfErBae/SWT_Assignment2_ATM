using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvent
{
    public interface IWindow
    {
        void Close();
        void Open();
        bool RunSelfTest();
    }
}
