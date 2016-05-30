using KnkInterfaces.Classes;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkCore
{
    public class KnkEntityIdentifier<TDad, TReference> : KnkEntityIdentifier, KnkEntityIdentifierItf<TDad, TReference>
        where TDad : KnkItemItf
        where TReference : KnkItemItf, new()
    {
        private TReference _reference;
        private Func<int?, TReference> Load { get; set; }
        private string _property;

        public KnkEntityIdentifier(TDad aItem, string aProperty)
        : this(aItem, aProperty,aItem.Connection().GetItem<TReference>)
        {
        }

        public KnkEntityIdentifier(TDad aItem, string aProperty, Func<int?, TReference> aLoad)
        {
            ResetReference(aItem, aProperty, aLoad);
        }

        public void ResetReference(TDad aItem, string aProperty)
        {
            _property = aProperty;
            _reference = default(TReference);
        }

        public void ResetReference(TDad aItem, string aProperty, Func<int?, TReference> aLoad)
        {
            Release();
            KnkEntityIdentifier lValue = aItem.PropertyGet(aProperty) as KnkEntityIdentifier;
            SetInnerValue(lValue);
            Load = aLoad;
        }

        #region member variables

        public TReference Value
        {
            get
            {
                int? lValue = this.GetInnerValue();
                if (lValue != null && lValue.HasValue && Load != null) _reference = Load(lValue);
                return _reference;
            }
            set
            {
                if (value != null)
                {
                    this._reference = value;
                    string lVal = this._reference?.PrimaryKey();
                    if (!string.IsNullOrEmpty(lVal))
                    {
                        KnkEntityIdentifier lEid = this._reference?.PropertyGet(lVal) as KnkEntityIdentifier;
                        if (lEid != null) this.SetInnerValue(lEid.GetInnerValue());
                    }
                }
            }
        }
        #endregion

        #region properties

        public void Release()
        {
            this.SetInnerValue(null);
            Load = null;
            _reference = default(TReference);
        }
        #endregion


    }
}
