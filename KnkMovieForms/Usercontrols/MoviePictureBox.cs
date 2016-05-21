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

        public string Filename
        {
            get
            {
                return _Filename;
            }

            set
            {
                _Filename = value;
                Thread lThr = new Thread(new ThreadStart(LoadPicture));
                lThr.Start();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintCenteredText(e.Graphics);
        }

        private void PaintCenteredText(Graphics aGraphics)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var lRectangle = this.ClientRectangle;
                StringFormat lStringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                var lFontBase = new Font("Verdana", 10, FontStyle.Bold);
                SizeF lSize = aGraphics.MeasureString(Text, lFontBase);
                float lFontScale = Math.Max(lSize.Width / lRectangle.Width, lSize.Height / lRectangle.Height);
                using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, FontStyle.Bold, GraphicsUnit.Point))
                {
                    aGraphics.DrawString(Text, lFont, Brushes.White, lRectangle, lStringFormat);
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
