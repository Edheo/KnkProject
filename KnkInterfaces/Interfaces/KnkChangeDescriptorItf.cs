using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkChangeDescriptorItf
    {
        [Browsable(false)]
        KnkEntityIdentifierItf IdValue { get; }
        [Browsable(false)]
        KnkItemItf Item { get; }
        string Action { get; }
        string Object { get; }
        string Text { get; }
        string Message { get; }

        DateTime? CreationDate { get; }
        DateTime? ModifiedDate { get; }
        [Browsable(false)]
        DateTime? DeletedDate { get; }
    }
}
