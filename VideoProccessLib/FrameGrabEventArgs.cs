using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoProccessLib
{
    public class FrameGrabEventArgs: EventArgs
    {
        public Mat MatImage;
        public double FPS;
        public double RealFPS;
        public DateTime TimeStartGetFrame;

        public FrameGrabEventArgs(Mat matImage, DateTime timeStartGetFrame, double fps = 0, double realFPS = 0): base() {
            MatImage = matImage;
            FPS = fps;
            RealFPS = realFPS;
            TimeStartGetFrame = timeStartGetFrame;
        }
    }
}
