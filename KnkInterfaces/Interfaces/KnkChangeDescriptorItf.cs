using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkChangeDescriptorItf
    {
        KnkEntityIdentifierItf IdValue { get; }
        string Action { get; }
        KnkItemItf Item { get; }
        string Object { get; }
        string Information { get; }
        DateTime? CreationDate { get; }
        DateTime? ModifiedDate { get; }
        DateTime? DeletedDate { get; }
    }
}
