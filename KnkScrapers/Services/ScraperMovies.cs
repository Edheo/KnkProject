using KnkInterfaces.Interfaces;
using KnkScrapers.Classes;
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
            EnrichCollections lCols = new EnrichCollections(_Files.Connection);
            foreach(var lFil in _Files.Items)
            {
                ScrapFile(lFil, lCols);
            }
        }

        private void ScrapFile(File aFile, EnrichCollections aCols)
        {
            List<System.Net.TMDb.Movie> lResult = KnkScraperTmdb.FindMovies(aFile, "es-ES");
            Movies lMovies = new Movies(_Files.Connection);
            foreach (var lItm in lResult)
            {
                Movie lMov = KnkScraperTmdb.FindMovieInLibrary(lMovies, lItm);
                lMov = KnkScraperTmdb.EnrichMovie(aCols, lItm, lMov);

            }
        }

        

    }
}
