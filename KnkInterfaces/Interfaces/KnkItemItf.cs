﻿using KnkInterfaces.Enumerations;
using System;
using System.Reflection;

namespace KnkInterfaces.Interfaces
{
    public interface KnkItemItf
    {
        KnkItemItf Load<T>(int aId) where T : KnkItemItf, new();


        string PrimaryKey();
        bool PrimaryKeyAutoGenerated();
        object PropertyGet(string aProperty);
        void PropertySet(string aProperty, object aValue);

        KnkListItf GetParent();
        void SetParent(KnkListItf aParent);

        KnkConnectionItf Connection();
        KnkTableEntityItf SourceEntity();

        KnkEntityIdentifierItf UserCreationId { get; set; }
        KnkEntityIdentifierItf UserModifiedId { get; set; }
        KnkEntityIdentifierItf UserDeletedId { get; set; }

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
