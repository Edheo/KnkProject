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

        public void FillGenres(Movie aMovie, List<string> aGenres)
        {
            aMovie.Genres().DeleteAll();
            foreach (var lGenre in aGenres)
            {
                var lFound = aMovie.Genres().Items.Where(g => g.GenreName.ToLower().Equals(lGenre.ToLower())).FirstOrDefault();
                if (lFound==null)
                {
                    lFound = CheckGenre(lGenre);
                }
                lFound.Update();
            }
        }

        public Genre CheckGenre(string aGenre)
        {
            var lGen = Genres.Items.Where(g => g.GenreName.ToLower().Equals(aGenre.ToLower())).FirstOrDefault();
            if (lGen == null)
            {
                lGen = Genres.Create();
                lGen.GenreName = aGenre;

            }
            return lGen;
        }


    }
}
