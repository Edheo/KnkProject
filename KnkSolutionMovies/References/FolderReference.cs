using KnkCore;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.References
{
    class FolderReference : KnkEntityIdentifier<Folder, Folder>
    {
        public FolderReference(Folder aFolder, string aProperty) 
        : base(aFolder, aProperty, aFolder.Connection().GetItem<Folder>)
        {
        }

        public string Text
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
