using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Image _ImageAnimation;

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

        public new string Text { get { return base.Text; } set { base.Text = value; } }

        public Brush FontBrush { get { return _FontBrush; } set { _FontBrush = value; } }

        public Image ImageAnimation { get { return _ImageAnimation; } set { _ImageAnimation = value; } }

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
            if (!string.IsNullOrEmpty(Text))
            {
                var lRectangle = this.ClientRectangle;
                var lFontBase = new Font(FontName, 10, this.Fontstyle);
                var lSize = aGraphics.MeasureString(Text, lFontBase);
                var lFontScale = Math.Max(lSize.Width / lRectangle.Width, lSize.Height / lRectangle.Height);
                using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.Fontstyle, GraphicsUnit.Point))
                {
                    aGraphics.DrawString(Text, lFont, Brushes.White, lRectangle, StringFormat());
                }
            }
        }

        private void LoadPicture()
        {
            if (!string.IsNullOrEmpty(_Filename))
            {
                Image lImg = Image.FromFile(_Filename);
                if (InvokeRequired)
                    this.Invoke(new delSetPicture(SetPicture), lImg);
                else
                    SetPicture(lImg);
            }
        }

        private void SetPicture(Image aImg)
        {
            MessageBox.Show("Change it to FromStream, instead of FromFile");
            this.Image = aImg;
        }

        public void AnimationStart()
        {
            //this.Enabled = false;
            this.Image = ImageAnimation;
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
            FrameDimension lDim = new FrameDimension(ImageAnimation.FrameDimensionsList[0]);
            ImageAnimation.SelectActiveFrame(lDim, 0);
            Bitmap lBmp = new Bitmap(new Bitmap((Image)this.ImageAnimation.Clone()));
            this.Image = lBmp;
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            // frame change
        }

        protected override void OnResize(EventArgs e)
        {
            if(this.Dock==DockStyle.Left || this.Dock==DockStyle.Right)
            {
                this.Size = new Size(this.Height, this.Height);
            }
        }
    }
}
