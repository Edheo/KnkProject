﻿using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkEntityIdentifierItf : IConvertible
    {
        int? GetInnerValue();
        void SetInnerValue(int? aValue);
    }

    public interface KnkEntityIdentifierItf<TDad, TReference> : KnkEntityIdentifierItf, KnkReferenceItf<TDad, TReference>
        where TDad : KnkItemItf
        where TReference : KnkItemItf, new()
    {
    }
}