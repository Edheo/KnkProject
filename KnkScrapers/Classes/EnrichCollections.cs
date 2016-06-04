using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkScrapers.Classes
{
    public class EnrichCollections
    {
        internal readonly Genres Genres;
        internal EnrichCollections(KnkConnectionItf aCon)
        {
            Genres = new Genres(aCon);
        }

        public Genre CheckGenre(string aGenre)
        {
            var lGen = Genres.Items.Where(g => g.GenreName.ToLower().Equals(aGenre.ToLower())).FirstOrDefault();
            if(lGen==null)
            {
                lGen = Genres.Create();
                lGen.GenreName = aGenre;

            }
            return lGen;
        }

        public void FillGenres(Movie aMovie, IEnumerable<string> aGenres)
        {
            //foreach(var lGen in aGenres)
            //{
            //    if(!aMovie.Extender.Genres)
            //    CheckGenre(lGen);
            //}
            //var lGen = Genres.Items.Where(g => g.GenreName.ToLower().Equals(aGenre.ToLower())).FirstOrDefault();
            //if (lGen == null)
            //{
            //    lGen = Genres.Create();
            //    lGen.GenreName = aGenre;

            //}
            //return lGen;
        }

    }
}
