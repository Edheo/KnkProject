using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnkSolutionMovies.Entities;

namespace KnkMovieForms.Usercontrols
{
    public partial class VideoThumb : MediaThumb
    {
        private MediaLink _Media;

        public MediaLink Media()
        {
            return _Media;
        }

        private VideoThumb()
        : base()
        {
        }

        public VideoThumb(MediaLink aMedia, int aWidth)
        : base(aWidth)
        {
            SetMedia(aMedia);
        }

        private void SetMedia(MediaLink aMedia)
        {
            _Media = aMedia;
            FileName = _Media.Extender.GetFileName();
            Horizontal = false;
            SetValues(String.Empty, aMedia.ToString());
        }
    }
}
