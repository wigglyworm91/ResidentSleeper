using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatJAM
{

    public partial class OutlineLabel : Label
    {
        private float borderSize;
        private Color borderColor;

        private PointF point;
        private SizeF drawSize;
        private Pen drawPen;
        private GraphicsPath drawPath;
        private SolidBrush forecolorBrush;

        public OutlineLabel()
        {
            this.borderSize = 1f;
            this.borderColor = Color.Black;
            this.drawPath = new GraphicsPath();
            this.drawPen = new Pen(new SolidBrush(this.borderColor), borderSize);
            this.forecolorBrush = new SolidBrush(this.ForeColor);

            this.Invalidate();
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The border's thickness")]
        [DefaultValue(1f)]
        public float BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                if (value == 0) {
                    this.drawPen.Color = Color.Transparent;
                } else {
                    this.drawPen.Color = this.BorderColor;
                    this.drawPen.Width = value;
                }

                this.OnTextChanged(EventArgs.Empty);
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The border's color")]
        [DefaultValue(typeof(Color), "White")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                this.borderColor = value;
                if (this.BorderSize != 0) {
                    this.drawPen.Color = value;
                }
                this.Invalidate();
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.Invalidate();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            this.Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            this.forecolorBrush.Color = base.ForeColor;
            base.OnForeColorChanged(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Text.Length == 0)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            this.drawSize = e.Graphics.MeasureString(this.Text, this.Font, new PointF(), StringFormat.GenericTypographic);

            if (this.AutoSize) {
                this.point.X = this.Padding.Left;
                this.point.Y = this.Padding.Top;
            } else {
                switch (this.TextAlign) {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        this.point.X = this.Padding.Left;
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        this.point.X = (this.Width - this.drawSize.Width) / 2;
                        break;
                    default:
                        this.point.X = this.Width - (this.Padding.Right + this.drawSize.Width);
                        break;
                }
                switch (this.TextAlign) {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        this.point.Y = this.Padding.Top;
                        break;
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleRight:
                        this.point.Y = (this.Height - this.drawSize.Height) / 2;
                        break;
                    default:
                        this.point.Y = this.Height - (this.Padding.Top+ this.drawSize.Height);
                        break;
                }
            }
            float fontSize = e.Graphics.DpiY * this.Font.SizeInPoints / 72;
            this.drawPath.Reset();
            this.drawPath.AddString(this.Text, this.Font.FontFamily, (int)this.Font.Style, fontSize, point, StringFormat.GenericTypographic);

            e.Graphics.FillPath(forecolorBrush, this.drawPath);
            e.Graphics.DrawPath(drawPen, this.drawPath);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                this.forecolorBrush?.Dispose();
                this.drawPath?.Dispose();
                this.drawPen?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}