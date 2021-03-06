﻿using KnkCore;
using KnkInterfaces.Interfaces;
using System;
using System.Reflection;

namespace KnkSolutionMovies.Utilities
{
    [Serializable]
    public class KnkDataModeler : KnkConfigurer, KnkDataModelerItf
    {
        private Assembly _Assembly = Assembly.GetAssembly(typeof(KnkDataModeler));

        public Assembly Assembly { get { return _Assembly; } }

        public override string Name { get { return _Assembly.GetName().CodeBase; } }

        public Version Version { get { return _Assembly.GetName().Version; } }
    }
}
