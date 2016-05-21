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
    public class MoviePictureBox:PictureBox
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
            //e.Graphics.DrawLine(new Pen(Color.Red), new Point(this.Width, 0), new Point(this.Width, this.Height));
            //ControlPaint.DrawReversibleLine(new Point(this.Width, 0), new Point(this.Width, this.Height), Color.Red);

            //ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), Color.DarkGray, ButtonBorderStyle.Inset);
        }

        private void LoadPicture()
        {
            Image lImg = Image.FromFile(_Filename);
            if (InvokeRequired)
                this.Invoke(new delSetPicture(SetPicture), lImg);
            else
                SetPicture(lImg);
        }

        private void SetPicture(Image aImg)
        {
            this.Image = aImg;
        }


    }

}
