using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.References
{
    [DefaultProperty("Text")]
    public class MovieSetReference : KnkReference<MovieSet>
    {
        public MovieSetReference(Movie aMovie, string aProperty) : base(null, aMovie.Connection.GetItem<MovieSet>)
        {
            int? lVal = (int?)aMovie.PropertyGet(aProperty);
            aMovie.Connection.SetReference<MovieSet>(this, lVal);
        }

        public string Text
        {
           get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return this.Value.Name;
        }
    }
}
