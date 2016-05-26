using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnkMovieForms.Usercontrols
{
    class MoviePictureBox:PictureBox
    {
        delegate void delNoParams();
        delegate void delSetPicture(Image aImg);
        private string _Filename;
        private string _FontName= "Verdana";
        private FontStyle _Fontstyle = FontStyle.Bold;
        private StringAlignment _TextAlignment = StringAlignment.Center;
        private StringAlignment _LineAlignment = StringAlignment.Center;
        private Brush _FontBrush = Brushes.White;
        private Image _ResourceImage;
        private int? _FontSize;
        private string _Caption;
        private float _Factor;

        public string Filename
        {
            get
            {
                return _Filename;
            }

            set
            {
                _Filename = value;
                if (!string.IsNullOrEmpty(_Filename))
                {
                    Thread lThr = new Thread(new ThreadStart(LoadPicture));
                    lThr.Start();
                }
            }
        }

        public StringAlignment TextAlignment { get { return _TextAlignment; } set { _TextAlignment = value; } }

        public StringAlignment LineAlignment { get { return _LineAlignment; } set { _LineAlignment = value; } }

        public string FontName { get { return _FontName; } set { _FontName = value; } }

        public FontStyle Fontstyle { get { return _Fontstyle; } set { _Fontstyle = value; } }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
                if (InvokeRequired)
                    this.Invoke(new delNoParams(this.Refresh));
                else
                    this.Refresh();

            }
        }

        public new string Text { get { return base.Text; } set { base.Text = value; } }

        public Brush FontBrush { get { return _FontBrush; } set { _FontBrush = value; } }

        public Image ResourceImage { get { return _ResourceImage; } set { _ResourceImage = value; } }

        public int? FontSize { get { return _FontSize; } set { _FontSize = value; } }

        public float Factor { get { return _Factor; } set { _Factor = value; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintCenteredText(e.Graphics);
        }

        public StringFormat StringFormat() 
        {
            return new StringFormat()
            {
                Alignment = TextAlignment,
                LineAlignment = LineAlignment
            };
        }

        private void PaintCenteredText(Graphics aGraphics)
        {
            var lRectangle = this.ClientRectangle;
            if (!string.IsNullOrEmpty(Caption) && StringFormat().LineAlignment==StringAlignment.Far)
            {
                var lFormat = StringFormat();
                lFormat.LineAlignment = StringAlignment.Near;
                using (var lFontBase = new Font(FontName, 10, this.Fontstyle))
                {
                    var lSize = aGraphics.MeasureString(Caption, lFontBase);
                    float lFontScale = 1;
                    if (lSize.Width>lRectangle.Width) lFontScale = lSize.Width / lRectangle.Width;
                    using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.Fontstyle, GraphicsUnit.Point))
                    {
                        aGraphics.DrawString(Caption, lFont, Brushes.White, lRectangle, lFormat);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Text))
            {
                if (_FontSize == null)
                {
                    using (var lFontBase = new Font(FontName, 10, this.Fontstyle))
                    {
                        var lSize = aGraphics.MeasureString(Text, lFontBase);
                        var lFontScale = Math.Max(lSize.Width / lRectangle.Width, lSize.Height / lRectangle.Height);
                        using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.Fontstyle, GraphicsUnit.Point))
                        {
                            aGraphics.DrawString(Text, lFont, Brushes.White, lRectangle, StringFormat());
                        }
                    }
                }
                else
                {
                    using (Font lFont = new Font(FontName, (int)_FontSize, this.Fontstyle))
                    {
                        aGraphics.DrawString(Text, lFont, Brushes.White, lRectangle, StringFormat());
                    }
                }

            }
        }

        private void LoadPicture()
        {
            if (!string.IsNullOrEmpty(_Filename))
            {
                using (var lStream = System.IO.File.OpenRead(_Filename))
                {
                    _ResourceImage = ResizeImage(Image.FromStream(lStream), this.ClientSize.Width, this.ClientSize.Height);
                }
                if (InvokeRequired)
                    this.Invoke(new delNoParams(SetPicture));
                else
                    SetPicture();
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void SetPicture()
        {
            this.Image = _ResourceImage;
        }

        public void AnimationStart()
        {
            //this.Enabled = false;
            Image = ResourceImage;
        }

        public void AnimationStop()
        {
            if (InvokeRequired)
                this.Invoke(new delNoParams(AnimationStoper));
            else
                AnimationStoper();
            //this.Enabled = true;
        }

        private void AnimationStoper()
        {
            FrameDimension lDim = new FrameDimension(ResourceImage.FrameDimensionsList[0]);
            ResourceImage.SelectActiveFrame(lDim, 0);
            Bitmap lBmp = new Bitmap(new Bitmap((Image)this.ResourceImage.Clone()));
            this.Image = lBmp;
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            // frame change
        }

        protected override void OnResize(EventArgs e)
        {
            if (Factor!=0 && (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right))
            {
                this.Size = new Size((int)(this.Height * _Factor), this.Height);
            }
            else
            if (Factor != 0 && (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom))
            {
                this.Size = new Size(this.Width, (int)(this.Width * _Factor));
            }
        }

        public void ReMarkMovie()
        {
            this.OnClick(new EventArgs());
            //this.Padding = new Padding(5, 5, 5, 5);
            //lPic.Dock = DockStyle.Fill;
            PictureBox lPic = new PictureBox();
            this.Image = null;
            this.BackColor = Color.Red;
            lPic.SizeMode = PictureBoxSizeMode.StretchImage;
            lPic.Image = _ResourceImage;
            lPic.Location = new Point(3, 3);
            lPic.Size = new Size(this.Width - 6, this.Height - 6);
            lPic.Click += (sender, e) => { this.OnClick(e); };
            this.Controls.Add(lPic);
        }

        public void RemoveBorder()
        {
            this.Controls.Clear();
            this.Image = _ResourceImage;
        }

        public bool HasBorder()
        {
            return this.Controls.Count > 0;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
