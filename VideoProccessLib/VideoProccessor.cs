using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace VideoProccessLib
{
    public class VideoProccessor
    {
        public delegate void ImageGrabEventHandle(object? sender, FrameGrabEventArgs e);
        public event ImageGrabEventHandle ImageGrabEvent;

        private VideoCapture? _capture;
        private DateTime lastTimeGetFrame = DateTime.MinValue;
        public bool CreateCapture => _capture != null;

        public VideoProccessor() { }

        /// <summary>
        /// Захват видеопотока из файла
        /// </summary>
        /// <param name="fileName">Путь до файла с видео</param>
        public void CaptureFromFile(string fileName)
        {
            StopCapture();
            _capture= new VideoCapture(fileName);
            _capture.ImageGrabbed += _capture_ImageGrabbed;
            _capture.Start();
        }

        public void CaptureFromGstreamer(string pipeLine)
        {
            StopCapture();
            _capture = new VideoCapture(pipeLine, VideoCapture.API.Gstreamer);
            _capture.ImageGrabbed += _capture_ImageGrabbed;
            _capture.Start();
        }

        private double GetRealFPS()
        {
            var time = DateTime.Now;
            double fps = 1000d / (time - lastTimeGetFrame).TotalMilliseconds;
            lastTimeGetFrame = time;
            return fps;
        }

        private void _capture_ImageGrabbed(object? sender, EventArgs e)
        {
            var time = DateTime.Now;
            Mat mat = new Mat();
            _capture.Retrieve(mat);
            ImageGrabEvent?.Invoke(this, new FrameGrabEventArgs(mat, time, _capture.Get(Emgu.CV.CvEnum.CapProp.Fps), GetRealFPS()));
            mat.Dispose();
        }

        public void StopCapture()
        {
            if (CreateCapture)
            {
                _capture.ImageGrabbed -= _capture_ImageGrabbed;
                if (_capture.IsOpened)
                    _capture.Stop();
                _capture.Dispose();
            }
        }
    }
}
