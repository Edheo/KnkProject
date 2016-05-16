using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Classes
{
    public class KnkTableEntity : KnkTableEntityItf
    {
        public string PrimaryKey { get; set; }

        public string SourceTable { get; set; }
    }
}
