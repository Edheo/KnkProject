using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkCore
{
    public class KnkReference<T>:KnkReferenceItf<T> where T:KnkItemItf, new()
    {
        public KnkReference()
        {

        }

        #region constructors
        public KnkReference(int? aId, Func<int?, T> aLoad)
        {
            ResetReference(aId, aLoad);
        }
        #endregion

        #region member variables

        private T _value;
        private int? _id;

        private Func<int?, T> Load { get; set; }

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

        public T Value
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

        public void ResetReference(int? aId)
        {
            _value = default(T);
            _id = aId;
        }

        public void ResetReference(int? aId, Func<int?, T> aLoad)
        {
            Release();
            _id = aId;
            Load = aLoad;
        }

        #endregion

        #region overloaded operators

        public static implicit operator KnkReference<T>(T operand)
        {
            return new KnkReference<T> { Value = operand };
        }

        public static bool operator ==(KnkReference<T> le, T e)
        {
            KnkReference<KnkItemBase> lCast = le as KnkReference<KnkItemBase>;
            KnkItemBase lCaste = e as KnkItemBase;
            return (e == null && le == null ? true : e == null ? false : lCast.Value == lCaste || (le.Value.KnkEntityId.HasValue && e.KnkEntityId.HasValue && le.Value.KnkEntityId.Value == e.KnkEntityId.Value));
        }

        public static bool operator !=(KnkReference<T> le, T e)
        {
            return !(le == e);
        }

        #endregion

        #region methods

        public override bool Equals(object obj)
        {
            return (typeof(object) == typeof(KnkReference<T>)
                    && this == obj);
        }

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        public void Release()
        {
            Id = null;
            Load = null;
            _value = default(T);
        }


        #endregion
    }
}
