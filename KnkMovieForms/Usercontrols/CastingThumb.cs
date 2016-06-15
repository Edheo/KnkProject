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

        public CastingThumb(MovieCasting aCasting, int aWidth)
        : base(aWidth)
        {
            SetCasting(aCasting);
        }

        private void SetCasting(MovieCasting aCasting)
        {
            _Casting = aCasting;
            FileName = _Casting.IdCasting.Reference.Extender.Poster?.Extender.GetFileName();
            SetValues(string.Empty, aCasting.ToString());
        }
    }
}
