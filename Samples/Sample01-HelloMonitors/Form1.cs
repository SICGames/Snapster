using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.HellStormGames.Imaging.ScreenCapture;

namespace Sample01_HelloMonitors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            DisplayMonitor displayMonitor = new DisplayMonitor();
            MonitorCollection monitors = await displayMonitor.GetMonitorsAsync();
            monitors.Dispose();
            displayMonitor.Dispose();

        }
    }
}
