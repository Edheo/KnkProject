using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using System;
using System.Reflection;

namespace KnkSolutionMovies.Utilities
{
    [Serializable]
    public class KnkDataModeler : KnkDataModelerItf
    {
        private Assembly _Assembly = Assembly.GetAssembly(typeof(KnkDataModeler));

        public Assembly Assembly
        {
            get
            {
                return _Assembly;
            }
        }

        public ConnectionTypeEnu ConnectionType { get; set; } = ConnectionTypeEnu.SqlServer;

        public string Database { get; set; }

        public string Name
        {
            get
            {
                return _Assembly.GetName().CodeBase;
            }
        }

        public string Password { get; set; }

        public string ServerPath { get; set; }

        public string User { get; set; }

        public Version Version
        {
            get
            {
                return _Assembly.GetName().Version;
            }
        }
    }
}
