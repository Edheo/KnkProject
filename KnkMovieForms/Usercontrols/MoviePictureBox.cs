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
