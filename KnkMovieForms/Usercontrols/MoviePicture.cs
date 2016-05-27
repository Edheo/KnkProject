using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using KnkMovieForms.Utilities;
using System.Threading;

namespace KnkMovieForms.Usercontrols
{
    public partial class MoviePicture : UserControl
    {
        delegate void delNoParams();
        delegate void delSetPicture(Image aImg);
        private string _Filename;
        private string _Text;
        private string _FontName = "Verdana";
        private FontStyle _FontstyleCaption = FontStyle.Bold;
        private FontStyle _FontstyleText = FontStyle.Regular;
        private StringAlignment _TextAlignment = StringAlignment.Center;
        private StringAlignment _LineAlignment = StringAlignment.Center;
        private Color _FontColorCaption = Color.White;
        private Color _FontColorText = Color.White;
        private Image _ResourceImage;
        private int? _FontSize;
        private string _Caption;
        private float _Factor;
        private PictureBoxSizeMode _SizeMode;


        public MoviePicture()
        {
            InitializeComponent();
            //this.picValue.Paint += (sender, e) => { PaintCenteredText(e.Graphics); };
        }
        
        [Description("TextAlignment"), Category("Data")]
        public StringAlignment TextAlignment { get { return _TextAlignment; } set { _TextAlignment = value; } }
        public StringAlignment LineAlignment { get { return _LineAlignment; } set { _LineAlignment = value; } }
        public string FontName { get { return _FontName; } set { _FontName = value; } }
        public FontStyle FontstyleCaption { get { return _FontstyleCaption; } set { _FontstyleCaption = value; } }
        public FontStyle FontstyleText { get { return _FontstyleText; } set { _FontstyleText = value; } }
        [Description("Caption"), Category("Data")]
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
        public new string Text { get { return _Text; } set { _Text = value; } }
        public PictureBoxSizeMode SizeMode { get { return _SizeMode; } set { _SizeMode = value; } }
        public Color FontColorCaption { get { return _FontColorCaption; } set { _FontColorCaption = value; } }
        public Color FontColorText { get { return _FontColorText; } set { _FontColorText = value; } }
        public Image ResourceImage { get { return _ResourceImage; } set { _ResourceImage = KnkMovieFormsUtils.ResizeImage(value,this.ClientSize.Width,this.ClientSize.Height); } }
        public int? FontSize { get { return _FontSize; } set { _FontSize = value; } }

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


        public void Factor (Size aSize)
        {
            _Factor = (float)aSize.Width / aSize.Height;
        }
        
        public float FactorSize { get { return _Factor; } set { _Factor = value; } }

        public StringFormat StringFormat()
        {
            return new StringFormat()
            {
                Alignment = TextAlignment,
                LineAlignment = LineAlignment
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(!string.IsNullOrEmpty(_Filename) && _ResourceImage!=null)
            {
                e.Graphics.DrawImage(_ResourceImage, 0, 0, ClientSize.Width, ClientSize.Height);
            }
            PaintCenteredText(e.Graphics);
        }

        private void PaintCenteredText(Graphics aGraphics)
        {
            var lRectangle = ClientRectangle;
            if (!string.IsNullOrEmpty(Caption) && StringFormat().LineAlignment == StringAlignment.Far)
            {
                var lFormat = StringFormat();
                lFormat.LineAlignment = StringAlignment.Near;
                using (var lFontBase = new Font(FontName, 10, this.FontstyleCaption))
                {
                    var lSize = aGraphics.MeasureString(Caption, lFontBase);
                    float lFontScale = 1;
                    if (lSize.Width > lRectangle.Width) lFontScale = lSize.Width / lRectangle.Width;
                    using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.FontstyleCaption, GraphicsUnit.Point))
                    {
                        aGraphics.DrawString(Caption, lFont, new SolidBrush(FontColorCaption), lRectangle, lFormat);
                    }
                    lRectangle.Size = new Size(lRectangle.Size.Width, lRectangle.Size.Height - (int)lSize.Height);
                    lRectangle.Location = new Point(lRectangle.Location.X, lRectangle.Location.Y + (int)lSize.Height);
                }
            }
            if (!string.IsNullOrEmpty(Text))
            {
                if (_FontSize == null)
                {
                    using (var lFontBase = new Font(FontName, 10, this.FontstyleText))
                    {
                        var lSize = aGraphics.MeasureString(Text, lFontBase);
                        var lFontScale = Math.Max(lSize.Width / lRectangle.Width, lSize.Height / lRectangle.Height);
                        using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.FontstyleText, GraphicsUnit.Point))
                        {
                            aGraphics.DrawString(Text, lFont, new SolidBrush(FontColorText), lRectangle, StringFormat());
                        }
                    }
                }
                else
                {
                    using (Font lFont = new Font(FontName, (int)_FontSize, this.FontstyleText))
                    {
                        aGraphics.DrawString(Text, lFont, new SolidBrush(FontColorText), lRectangle, StringFormat());
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
                    ResourceImage = Image.FromStream(lStream);
                }
                if (InvokeRequired)
                    this.Invoke(new delNoParams(SetPicture));
                else
                    SetPicture();
            }
            else if(_ResourceImage!=null)
            {
                SetPicture();
            }
        }

        private void SetPicture()
        {
            this.BackgroundImage = _ResourceImage;
        }

        public void AnimationStart()
        {
            this.BackgroundImage = ResourceImage;
        }

        public void AnimationStop()
        {
            if (InvokeRequired)
                this.Invoke(new delNoParams(AnimationStoper));
            else
                AnimationStoper();
        }

        private void AnimationStoper()
        {
            FrameDimension lDim = new FrameDimension(ResourceImage.FrameDimensionsList[0]);
            ResourceImage.SelectActiveFrame(lDim, 0);
            Bitmap lBmp = new Bitmap(new Bitmap((Image)this.ResourceImage.Clone()));
            this.BackgroundImage = lBmp;
        }

        protected override void OnResize(EventArgs e)
        {
            if (_Factor != 0 && (Dock == DockStyle.Left || Dock == DockStyle.Right))
            {
                Size = new Size((int)(this.Height * _Factor), this.Height);
                LoadPicture();
            }
            else
            if (_Factor != 0 && (Dock == DockStyle.Top || this.Dock == DockStyle.Bottom))
            {
                Size = new Size(this.Width, (int)(this.Width * _Factor));
                LoadPicture();
            }
        }

        public void ReMarkMovie()
        {
            //this.OnClick(new EventArgs());
            this.Padding = new Padding(5, 5, 5, 5);
            PictureBox lPic = new PictureBox();
            lPic.Dock = DockStyle.Fill;
            this.BackColor = Color.Red;
            lPic.SizeMode = PictureBoxSizeMode.StretchImage;
            lPic.Image = BackgroundImage;
            lPic.Location = new Point(3, 3);
            lPic.Size = new Size(this.Width - 6, this.Height - 6);
            lPic.Click += (sender,e) => { OnClick(e); };
            this.Controls.Add(lPic);
            BackgroundImage = null;
        }

        public void RemoveBorder()
        {
            this.Controls.Clear();
            this.BackgroundImage = _ResourceImage;
        }

        public bool HasBorder()
        {
            return this.Controls.Count > 0;
        }

    }
}
