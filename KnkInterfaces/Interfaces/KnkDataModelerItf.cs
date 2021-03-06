﻿using KnkInterfaces.Enumerations;
using System;
using System.Reflection;

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

        string MediaFolder { get; set; }
    }

    public interface KnkDataModelerItf : KnkConfigurationItf
    {
        Version Version { get; }

        Assembly Assembly { get; }
    }
}
