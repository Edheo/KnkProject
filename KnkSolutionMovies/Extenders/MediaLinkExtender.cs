using KnkCore;
using KnkCore.Utilities;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Utilities;
using System.IO;
using System.Xml;

namespace KnkSolutionMovies.Extenders
{
    public class MediaLinkExtender
    {
        private readonly MediaLink _MediaLink;

        public object Connection { get; private set; }

        public MediaLinkExtender(MediaLink aMediaThumb)
        {
            _MediaLink = aMediaThumb;
        }

        public string GetImageUri()
        {
            XmlDocument lXml = new XmlDocument();
            lXml.LoadXml(_MediaLink.Link);
            return lXml.InnerText;
        }

        public string GetFileName()
        {
            string lFrom = GetImageUri();
            KnkConnection lConf = _MediaLink.Connection() as KnkConnection;
            string lFolder = lConf.Configuration().GetMediaFolder(typeof(Movie));

            string lPartName1 = _MediaLink.IdMovie.Value?.Extender.ParsedId();
            //string lPartName2 = KnkSolutionMoviesUtils.GetLastPart(lFrom, '/');
            string lPartName2 = KnkSolutionMoviesUtils.GetLastPart(lFrom, '/');

            string lFileName = Path.Combine(lFolder, KnkCoreUtils.CleanFileName($"{lPartName1}_{lPartName2}"));
            if (!System.IO.File.Exists(lFileName))
            {
                KnkSolutionMoviesUtils.WriteStreamToFile(KnkSolutionMoviesUtils.GetUrlStream(lFrom), lFileName);
            }
            return lFileName;
        }

    }
}
