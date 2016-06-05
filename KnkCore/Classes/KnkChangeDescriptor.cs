using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnkInterfaces.Enumerations;

namespace KnkCore
{
    public class KnkChangeDescriptor : KnkChangeDescriptorItf
    {
        public KnkChangeDescriptor(KnkItemItf aItm)
        {
            Item = aItm;
            IdValue = Item.PropertyGet(aItm.PrimaryKey()) as KnkEntityIdentifier;
            CreationDate = aItm.CreationDate;
            DeletedDate = aItm.DeletedDate;
            ModifiedDate = aItm.ModifiedDate;
            Object = aItm.GetType().Name;
            Action = aItm.Status().ToString();
            Information = aItm.ToString();
        }

        public string Object { get; }
        public KnkItemItf Item { get; }
        public KnkEntityIdentifierItf IdValue { get; }
        public DateTime? CreationDate { get; }
        public DateTime? DeletedDate { get; }
        public DateTime? ModifiedDate { get; }
        public string Information { get; }
        public string Action { get; }
    }
}
