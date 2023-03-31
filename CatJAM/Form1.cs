using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatJAM
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();
        private int i = 0;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Opacity = 0.9;
            this.TopMost = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = this.BackColor;
            

            timer.Interval = 5000;
            timer.Tick += new EventHandler(this.update);
            update(null, null);
            timer.Start();
        }

        private void update(object sender, EventArgs e)
        {
            var res = Screen.PrimaryScreen.Bounds;
            this.thelabel.Text = this.getIpAddresses();
            this.Width = this.thelabel.Width;
            this.Height = this.thelabel.Height;
            this.Location = new Point(50, res.Height - this.Height - 50);
        }

        private string getIpAddresses()
        {
            return "example\ntext";
            var host = Dns.GetHostName();
            return string.Join("\n", Dns.GetHostAddresses(host).Select(x => x.ToString()));
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_LAYERED = 0x8_0000;
                const int WS_EX_TRANSPARENT = 0x20;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing lol
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 0.1;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.4;
        }
    }
}
