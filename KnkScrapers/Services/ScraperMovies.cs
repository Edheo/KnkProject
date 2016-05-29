using KnkInterfaces.Interfaces;
using KnkScrapers.Utilities;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System.Collections.Generic;

namespace KnkScrapers.Services
{
    public class ScraperMovies
    {
        private readonly MissingMovies _Files;

        public ScraperMovies(KnkConnectionItf aCon)
        {
            _Files = new MissingMovies(aCon);
        }

        public void ScrapFiles()
        {
            foreach(var lFil in _Files.Items)
            {
                ScrapFile(lFil);
            }
        }

        private void ScrapFile(File aFile)
        {
            List<System.Net.TMDb.Movie> lResult = KnkScraperTmdb.FindMovies(aFile);
            foreach(var lItm in lResult)
            {
                string lMov = lItm.Title;
            }
        }

    }
}
