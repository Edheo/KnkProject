using KnkCore.Utilities;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;

namespace KnkCore
{
    public class KnkEntityReference<Tref> : KnkEntityIdentifier, KnkEntityReferenceItf<Tref>
        where Tref : KnkItemItf, new()
    {
        private Tref _reference;
        private Func<int?, Tref> Load { get; set; }

        public KnkEntityReference(int? aValue)
        : this(aValue, (new Tref()).Connection().GetItem<Tref>)
        {
        }

        public KnkEntityReference(Tref aItem)
        : this(aItem, (new Tref()).Connection().GetItem<Tref>)
        {
        }

        public KnkEntityReference(int? aValue, Func<int?, Tref> aLoad) 
        : base(aValue)
        {
            Load = aLoad;
        }

        public KnkEntityReference(Tref aItem, Func<int?, Tref> aLoad) 
        : base()
        {
            Load = aLoad;
            Reference = aItem;
        }

        #region member variables

        public override int? Value
        {
            get
            {
                if (_reference != null) base.Value = _reference.PrimaryKeyValue().Value;
                return base.Value;
            }

            set
            {
                Release();
                base.Value = value;
            }
        }

        public Tref Reference
        {
            get
            {
                if (Value != null && _reference == null && Load != null) _reference = Load(Value);
                return _reference;
            }

            private set
            {
                Release();
                _reference = value;
                base.Value = this.Value;
            }
        }
        #endregion

        private void Release()
        {
            base.Value = null;
            _reference = default(Tref);
        }

        public override string ToString()
        {
            return Reference?.ToString();
        }

        public static implicit operator KnkEntityReference<Tref>(int value)
        {
            return new KnkEntityReference<Tref>(value);
        }

        public static implicit operator KnkEntityReference<Tref>(int? value)
        {
            return new KnkEntityReference<Tref>(value);
        }

        public static implicit operator int(KnkEntityReference<Tref> value)
        {
            return (int?)value?.Value ?? 0;
        }

        public static implicit operator int?(KnkEntityReference<Tref> value)
        {
            return value?.Value;
        }

        public static implicit operator KnkEntityReference<Tref>(Tref value)
        {
            return new KnkEntityReference<Tref>(value);
        }

        public static implicit operator Tref(KnkEntityReference<Tref> value)
        {
            return value.Reference;
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

    }
}
