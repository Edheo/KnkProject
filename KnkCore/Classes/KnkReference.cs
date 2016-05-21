using KnkInterfaces.Interfaces;
using System;

namespace KnkCore
{
    public class KnkReference<TDad, TReference>:KnkReferenceItf<TDad, TReference> 
        where TDad:KnkItemItf
        where TReference:KnkItemItf, new()
    {
        public KnkReference()
        {

        }

        #region constructors
        public KnkReference(TDad aItem, string aProperty, Func<int?, TReference> aLoad)
        {
            ResetReference(aItem, aProperty, aLoad);
        }
        #endregion

        #region member variables

        private TReference _value;
        private string _property;
        private int? _id;

        private Func<int?, TReference> Load { get; set; }

        public int? Id
        {
            get
            {
                return _value != null ? _value.KnkEntityId : _id;
            }
            set
            {
            }
        }

        public TReference Value
        {
            get
            {
                if (_value == null && Id.HasValue && Load != null) _value = Load(Id.Value);
                return _value;
            }
            set
            {
                if (value != null)
                {
                    this._value = value;
                    Id = this._value?.KnkEntityId;
                }
            }
        }
        #endregion

        #region properties

        public void ResetReference(TDad aItem, string aProperty)
        {
            _property = aProperty;
            _value = default(TReference);
            _id = (int?)aItem.PropertyGet(aProperty);
        }

        public void ResetReference(TDad aItem, string aProperty, Func<int?, TReference> aLoad)
        {
            Release();
            _id = (int?)aItem.PropertyGet(aProperty);
            Load = aLoad;
        }

        #endregion

        #region overloaded operators

        //public static implicit operator KnkReference<TDad, TReference>(TReference operand)
        //{
        //    return new KnkReference<TDad, TReference> { Value = operand };
        //}

        //public static bool operator ==(KnkReference<TDad, TReference> le, TReference e)
        //{
        //    KnkReference<knki, KnkItemBase> lCast = le as KnkReference<KnkItemBase,KnkItemBase>;
        //    KnkItemBase lCaste = e as KnkItemBase;
        //    return (e == null && le == null ? true : e == null ? false : lCast.Value == lCaste || (le.Value.KnkEntityId.HasValue && e.KnkEntityId.HasValue && le.Value.KnkEntityId.Value == e.KnkEntityId.Value));
        //}

        //public static bool operator !=(KnkReference<TDad, TReference> le, TReference e)
        //{
        //    return !(le == e);
        //}

        #endregion

        #region methods

        //public override bool Equals(object obj)
        //{
        //    return (typeof(object) == typeof(KnkReference<TDad, TReference>)
        //            && this == obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        public void Release()
        {
            Id = null;
            Load = null;
            _value = default(TReference);
        }


        #endregion
    }
}
