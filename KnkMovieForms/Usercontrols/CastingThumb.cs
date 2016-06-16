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
    public partial class CastingThumb : MediaThumb
    {
        private MovieCasting _Casting;

        public MovieCasting Casting()
        {
            return _Casting;
        }

        private CastingThumb()
        : base()
        {
        }

        public CastingThumb(MovieCasting aCasting, int aWidth)
        : base(aWidth)
        {
            SetCasting(aCasting);
        }

        public CastingThumb(string aCastingType, int aWidth)
        : base(aWidth)
        {
            FileName = string.Empty;
            Horizontal = false;
            SetValues(aCastingType, string.Empty);
        }

        private void SetCasting(MovieCasting aCasting)
        {
            _Casting = aCasting;
            FileName = _Casting.IdCasting.Reference.Extender.Poster?.Extender.GetFileName();
            Horizontal = false;
            SetValues(String.Empty, aCasting.ToString());
        }
    }
}
