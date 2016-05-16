using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkTableEntityItf
    {
        string PrimaryKey { get; set; }
        string SourceTable { get; set; }
    }
}
