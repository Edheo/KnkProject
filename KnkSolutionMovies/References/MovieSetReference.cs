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
    public class MovieSetReference : KnkReference<Movie,MovieSet>
    {
        public MovieSetReference(Movie aMovie, string aProperty) : base(aMovie, aProperty, aMovie.Connection.GetItem<MovieSet>)
        {
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
