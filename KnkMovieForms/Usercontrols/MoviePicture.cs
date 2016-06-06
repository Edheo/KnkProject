using System;
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
        private Color _RemarkColor = Color.Red;
        private Image _ResourceImage;
        private float _Factor;
        private PictureBoxSizeMode _SizeMode;
        public bool _isButton;

        public MoviePicture()
        {
            InitializeComponent();
            //this.picValue.Paint += (sender, e) => { PaintCenteredText(e.Graphics); };
        }

        public PictureBoxSizeMode SizeMode { get { return _SizeMode; } set { _SizeMode = value; } }
        public Color RemarkColor
        {
            get { return _RemarkColor; }
            set
            {
                _RemarkColor = value;
                if (HasBorder())
                {
                    this.BackColor = value;
                }
            }
        }
        public Image ResourceImage
        {
            get { return _ResourceImage; }
            set
            {
                _ResourceImage = KnkMovieFormsUtils.ResizeImage(value, this.ClientSize.Width, this.ClientSize.Height);
            }
        }
        public bool IsButton
        {
            get
            {
                return _isButton;
            }
            set
            {
                _isButton = value;
                ReMarkMovie(value);
            }
        }

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!string.IsNullOrEmpty(_Filename) && _ResourceImage!=null)
            {
                e.Graphics.DrawImage(_ResourceImage, 0, 0, ClientSize.Width, ClientSize.Height);
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
            if(IsButton)
                ((PictureBox)this.Controls[0]).Image = _ResourceImage;
            else
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

        public void ReMarkMovie(bool aRemark)
        {
            if (aRemark && !HasBorder())
            {
                this.Padding = new Padding(5, 5, 5, 5);
                PictureBox lPic = new PictureBox();
                lPic.Dock = DockStyle.Fill;
                this.BackColor = _RemarkColor;
                lPic.SizeMode = PictureBoxSizeMode.StretchImage;
                lPic.Image = BackgroundImage;
                lPic.Location = new Point(3, 3);
                lPic.Size = new Size(this.Width - 6, this.Height - 6);
                lPic.Click += (sender, e) => { OnClick(e); };
                this.Controls.Add(lPic);
                BackgroundImage = null;
            }
            else if (!aRemark && HasBorder())
            {
                this.Controls.Clear();
                this.BackgroundImage = _ResourceImage;
            }
        }

        public bool HasBorder()
        {
            return this.Controls.Count > 0;
        }

    }
}
