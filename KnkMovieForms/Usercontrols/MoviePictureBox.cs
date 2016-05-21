using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnkMovieForms.Usercontrols
{
    class MoviePictureBox:PictureBox
    {
        delegate void delSetPicture(Image aImg);
        private string _Filename;
        private string _FontName= "Verdana";
        private FontStyle _Fontstyle = FontStyle.Bold;
        private StringAlignment _TextAlignment = StringAlignment.Center;
        private StringAlignment _LineAlignment = StringAlignment.Center;
        private Brush _FontBrush = Brushes.White;

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
            this.Image = aImg;
        }

    }
}
