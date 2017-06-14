using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLEngine
{
    class MainFunction
    {
        static void Main()
        {
            using (MainWindow window = new MainWindow())
            {
                //TODO right now only on default monitor; GetDisplay for all modes available!
                double l_refreshrate = OpenTK.DisplayDevice.GetDisplay(0).RefreshRate;
                window.Run();
            }
        }
    }
}
