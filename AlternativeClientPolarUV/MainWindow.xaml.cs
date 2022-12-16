using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using VideoProccessLib;
using Path = System.IO.Path;

namespace AlternativeClientPolarUV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VideoProccessor _videoProc = new VideoProccessor();
        public MainWindow()
        {
            InitializeComponent();

            _videoProc.ImageGrabEvent += _videoProc_ImageGrabEvent;
            _videoProc.CaptureFromFile(Path.Combine("data", "o1.mp4"));
            //_videoProc.CaptureFromGstreamer("udpsrc port=8000 ! gdpdepay ! rtph264depay ! decodebin ! autovideoconvert ! appsink sync=false");
        }

        private void _videoProc_ImageGrabEvent(object? sender, FrameGrabEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                fpsBlock.Text = e.FPS.ToString();
                realfpsBlock.Text = e.RealFPS.ToString();
                image.Source = e.MatImage.ToImageSource();
                timeBlock.Text = (DateTime.Now - e.TimeStartGetFrame).Milliseconds.ToString();
            });
        }
    }
}
