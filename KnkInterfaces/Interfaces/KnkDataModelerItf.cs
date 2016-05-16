using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkConfigurationItf
    {
        ConnectionTypeEnu ConnectionType { get; set; } 

        string Name { get; }

        string ServerPath { get; set; }

        string Database { get; set; }

        string User { get; set; }

        string Password { get; set; }
    }

    public interface KnkDataModelerItf : KnkConfigurationItf
    {
        Version Version { get; }

        Assembly Assembly { get; }
    }
}
