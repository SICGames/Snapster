using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.HellStormGames.Imaging;
using com.HellStormGames.Imaging.Extensions;
using com.HellStormGames.Imaging.ScreenCapture.Interlop;

namespace Sample03_HelloDexterMorgan
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var image = "dexter.jpg"; //-- change to whatever.
            Bitmap bitmap = (Bitmap)Image.FromFile(image);
            using (var imagedata = bitmap.ToImageData())
            {
                imagedata.Save("Hello-Mister-Morgan.jpg");
                pictureBox1.Image = imagedata.ToBitmap();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
