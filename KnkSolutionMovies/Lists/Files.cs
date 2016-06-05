﻿using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Files : KnkList<File>
    {
        public Files(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }

        public Files(KnkConnectionItf aConnection, KnkCriteriaItf<File, File> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

    }
}
