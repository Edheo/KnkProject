using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using KnkInterfaces.Enumerations;

namespace KnkCore
{
    [Serializable]
    class KnkConfigurer : KnkConfigurationItf
    {
        KnkDataItf _Conection;

        public ConnectionTypeEnu ConnectionType { get; set; } = ConnectionTypeEnu.SqlServer;

        public string Database { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string ServerPath { get; set; }

        public string User { get; set; }

        public bool IsConfigured()
        {
            return string.IsNullOrEmpty(ServerPath);
        }

        public KnkDataItf CreateConnection()
        {
            if (_Conection == null)
            {
                switch (this.ConnectionType)
                {
                    case ConnectionTypeEnu.SqlServer:
                        _Conection = new KnkDataSqlServer.Connection.KnkSqlConnection(this);
                        break;
                    default:
                        throw new Exception($"Connection type {ConnectionType} not implemented");
                }
            }
            return _Conection;
        }

    }
}
