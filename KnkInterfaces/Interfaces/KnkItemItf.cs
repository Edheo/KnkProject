using KnkInterfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkItemItf
    {
        KnkListItf Parent{ get; set; }
        KnkConnectionItf Connection { get; }
        KnkItemItf Load<T>(int aId) where T : KnkItemItf, new();
        KnkTableEntityItf SourceEntity { get; }
        int? KnkEntityId { get; set; }
        object PropertyGet(string aProperty);
        void PropertySet(string aProperty, object aValue);

        int? UserCreationId { get; set; }
        int? UserModifiedId { get; set; }
        int? UserDeletedId { get; set; }

        DateTime? CreationDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
