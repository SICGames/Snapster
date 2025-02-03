using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.HellStormGames.Imaging;
using com.HellStormGames.Imaging.Extensions;
using com.HellStormGames.Imaging.ScreenCapture;
using com.HellStormGames.Imaging.ScreenCapture.Interlop;

namespace Sample02_SmileForTheScreenShot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Snapster.Release();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Snapster.Capturer = new SnapsterConfiguration().CapturerContext.WindowsGDI(MonitorConfiguration.MainMonitor).CreateCapturer();
            var monitorconfig = Snapster.MonitorConfiguration;

            var region = new com.HellStormGames.Imaging.Region(250, 0, 1024, 1200);
            var image = Snapster.CaptureDesktop();
            image.Save("screen-shot-region.png");
            pictureBox1.Image = Image.FromFile("screen-shot-region.png");
            var screenRect = this.RectangleToScreen(this.ClientRectangle);
            var titlebarHeight = screenRect.Top - this.Top;
            this.Height = image.Height + titlebarHeight + 10;
            this.Width = image.Width + 15;
        }
    }
}
