using KnkSolutionMovies.Entities;
using KnkSolutionMovies.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Extenders
{
    public class FolderExtender
    {
        private readonly Folder _Folder;

        FolderReference _RootReference = null;
        FolderReference _ParentReference = null;

        public FolderExtender(Folder aFolder)
        {
            _Folder = aFolder;
        }

        #region Relationships
        private FolderReference RootFolderReference()
        {
            if (_RootReference == null)
                _RootReference = new FolderReference(_Folder, "IdRoot");
            return _RootReference;
        }

        private FolderReference ParentFolderReference()
        {
            if (_ParentReference == null)
                _ParentReference = new FolderReference(_Folder, "IdParentPath");
            return _ParentReference;
        }
        #endregion Relationships

        public Folder RootFolder
        {
            get
            {
                return RootFolderReference().Value;
            }
        }

        public Folder ParentFolder
        {
            get
            {
                return ParentFolderReference().Value;
            }
        }
    }
}
