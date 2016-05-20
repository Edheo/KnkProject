using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieThumb : UserControl
    {
        private Size _previousSize;

        public MovieThumb()
        {
            InitializeComponent();
            _previousSize = Size;
        }

        public override Size MaximumSize
        {
            get
            {
                Size lSiz = this.MinimumSize;
                return new Size(lSiz.Width * 2, lSiz.Height * 2);
            }

            set
            {
                base.MaximumSize = this.MaximumSize;
            }
        }

        float Aspect()
        {
            return (float)MinimumSize.Height / (float)MinimumSize.Width;
        }

        private void MovieThumb_SizeChanged(object sender, EventArgs e)
        {
            Size lCurrent = this.Size;
            Size lPrevious = _previousSize;
            _previousSize = this.Size;
            if (lCurrent.Width != lPrevious.Width || lCurrent.Height != lPrevious.Height)
            {
                this.Size = new Size(lCurrent.Width, (int)(lCurrent.Width * Aspect()));
            }
        }
    }
}
