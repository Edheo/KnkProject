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
        private System.ComponentModel.IContainer components;
        private Image _ResourceImage;
        private int? _FontSize;

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

        public Image ResourceImage { get { return _ResourceImage; } set { _ResourceImage = value; } }

        public int? FontSize { get { return _FontSize; } set { _FontSize = value; } }

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
                if (_FontSize == null)
                {
                    var lFontBase = new Font(FontName, 10, this.Fontstyle);
                    var lSize = aGraphics.MeasureString(Text, lFontBase);
                    var lFontScale = Math.Max(lSize.Width / lRectangle.Width, lSize.Height / lRectangle.Height);
                    using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.Fontstyle, GraphicsUnit.Point))
                    {
                        aGraphics.DrawString(Text, lFont, Brushes.White, lRectangle, StringFormat());
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
                _ResourceImage = Image.FromFile(_Filename);
                if (InvokeRequired)
                    this.Invoke(new delNoParams(SetPicture));
                else
                    SetPicture();
            }
        }

        private void SetPicture()
        {
            this.Image = _ResourceImage;
        }

        public void AnimationStart()
        {
            //this.Enabled = false;
            this.Image = ResourceImage;
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
            if(this.Dock==DockStyle.Left || this.Dock==DockStyle.Right)
            {
                this.Size = new Size(this.Height, this.Height);
            }
        }

        public void PutBorder()
        {
            //this.Padding = new Padding(5, 5, 5, 5);
            //lPic.Dock = DockStyle.Fill;
            PictureBox lPic = new PictureBox();
            this.Image = null;
            this.BackColor = Color.Red;
            lPic.SizeMode = PictureBoxSizeMode.StretchImage;
            lPic.Image = _ResourceImage;
            lPic.Location = new Point(3, 3);
            lPic.Size = new Size(this.Width - 6, this.Height - 6);
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
