using KnkCore;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Utilities;
using System.IO;
using System.Xml;

namespace KnkSolutionMovies.Extenders
{
    public class MediaThumbExtender
    {
        private readonly MediaThumb _MediaThumb;

        public object Connection { get; private set; }

        public MediaThumbExtender(MediaThumb aMediaThumb)
        {
            _MediaThumb = aMediaThumb;
        }

        public string GetImageUri()
        {
            XmlDocument lXml = new XmlDocument();
            lXml.LoadXml(_MediaThumb.Thumb);
            return lXml.InnerText;
        }

        public string GetFileName()
        {
            string lFrom = GetImageUri();
            KnkConnection lConf = _MediaThumb.Connection as KnkConnection;
            string lFolder = lConf.Configuration().GetMediaFolder(typeof(Movie));

            string lPartName1 = _MediaThumb.Connection.GetItem<Movie>(_MediaThumb.IdMovie).Extender.ParsedId();
            string lPartName2 = KnkUtility.GetLastPart(lFrom, '/');

            string lFileName = Path.Combine(lFolder, $"{lPartName1}_{lPartName2}");
            if (!System.IO.File.Exists(lFileName))
            {
                KnkUtility.WriteStreamToFile(KnkUtility.GetUrlStream(lFrom), lFileName);
            }
            return lFileName;
        }

    }
}
