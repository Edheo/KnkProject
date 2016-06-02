using KnkCore;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Utilities;
using System.IO;
using System.Xml;

namespace KnkSolutionMovies.Extenders
{
    public class MediaLinkExtender
    {
        private readonly MediaLinks _MediaThumb;

        public object Connection { get; private set; }

        public MediaLinkExtender(MediaLinks aMediaThumb)
        {
            _MediaThumb = aMediaThumb;
        }

        public string GetImageUri()
        {
            XmlDocument lXml = new XmlDocument();
            lXml.LoadXml(_MediaThumb.Link);
            return lXml.InnerText;
        }

        public string GetFileName()
        {
            string lFrom = GetImageUri();
            KnkConnection lConf = _MediaThumb.Connection() as KnkConnection;
            string lFolder = lConf.Configuration().GetMediaFolder(typeof(Movie));

            string lPartName1 = ParsedId();
            //string lPartName2 = KnkSolutionMoviesUtils.GetLastPart(lFrom, '/');
            string lPartName2 = KnkSolutionMoviesUtils.GetLastPart(lFrom, '/');

            string lFileName = Path.Combine(lFolder, $"{lPartName1}_{lPartName2}");
            if (!System.IO.File.Exists(lFileName))
            {
                KnkSolutionMoviesUtils.WriteStreamToFile(KnkSolutionMoviesUtils.GetUrlStream(lFrom), lFileName);
            }
            return lFileName;
        }

        public string ParsedId()
        {
            return _MediaThumb.IdMovie.ToString().PadLeft(8, '0');
        }

    }
}
