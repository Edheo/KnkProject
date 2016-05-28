using KnkInterfaces.Enumerations;
using System;
using System.Reflection;

namespace KnkInterfaces.Interfaces
{
    public interface KnkItemItf
    {
        KnkListItf Parent{ get; set; }
        KnkConnectionItf Connection { get; }
        KnkItemItf Load<T>(int aId) where T : KnkItemItf, new();
        KnkTableEntityItf SourceEntity { get; }


        string PrimaryKey();
        object PropertyGet(string aProperty);
        void PropertySet(string aProperty, object aValue);

        int? UserCreationId { get; set; }
        int? UserModifiedId { get; set; }
        int? UserDeletedId { get; set; }

        DateTime? CreationDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }

        T Clone<T>() where T : KnkItemItf, new();

        void Update();
        void Delete();

        UpdateStatusEnu Status();

        string ToString();
    }
}
