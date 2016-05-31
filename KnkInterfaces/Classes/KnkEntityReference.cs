using KnkInterfaces.Classes;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Classes
{
    public class KnkEntityReference<Tref> : KnkEntityIdentifierItf<Tref>
        where Tref : KnkItemItf, new()
    {
        private Tref _reference;
        private Func<int?, Tref> Load { get; set; }

        public KnkEntityReference(int? aValue)
        {
            //ResetReference(aItem, aLoad);
        }

        public KnkEntityReference(Tref aItem, Func<int?, Tref> aLoad)
        {
            ResetReference(aItem, aLoad);
        }

        public void ResetReference(Tref aItem)
        {
            _reference = default(Tref);
        }

        public void ResetReference(Tref aItem, Func<int?, Tref> aLoad)
        {
            Release();
            int? lValue = (int?)aItem?.PropertyGet(aItem.PrimaryKey());
            if (aItem!=null && lValue!=null)
                SetInnerValue(lValue);
            Load = aLoad;
        }

        public int? GetInnerValue()
        {
            var lRet = GetInnerValue();
            if (lRet == null && _reference != null)
            {
                lRet = (int?)_reference.PropertyGet(_reference.PrimaryKey());
            }
            return lRet;
        }
        #region member variables


        public Tref Value
        {
            get
            {
                int? lValue = this.GetInnerValue();
                if (lValue != null && lValue.HasValue && Load != null) 
                {
                    if(_reference == null) _reference = Load(lValue);
                }
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
            _reference = default(Tref);
        }
        #endregion

        private int? _value;

        public void SetInnerValue(int? aValue)
        {
            _value = aValue;
            Release();
        }

        public override string ToString()
        {
            return Value?.ToString();
        }

        public static implicit operator KnkEntityReference<Tref>(int value)
        {
            return new KnkEntityReference<Tref>(value);
        }

        public static implicit operator int(KnkEntityReference<Tref> value)
        {
            return (int?)value?._value??0;
        }

        public static implicit operator KnkEntityReference<Tref>(int? value)
        {
            return new KnkEntityReference<Tref>(value);
        }

        public static implicit operator int? (KnkEntityReference<Tref> value)
        {
            return value?._value;
        }

        public override bool Equals(object obj)
        {
            return _value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return (bool)Convert.ChangeType(_value ?? 0, typeof(bool), provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return (char)Convert.ChangeType(_value, typeof(char), provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (Int16)Convert.ChangeType(_value, typeof(Int16), provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return (UInt16)Convert.ChangeType(_value, typeof(UInt16), provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return _value ?? 0;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return _value?.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType.Equals(this.GetType()))
                return _value;
            else
                throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            int? lObj = KnkInterfacesUtils.ObjectToKnkInt(obj);

            if (lObj == this.GetInnerValue())
                return 0;
            else
                return 1;
        }


    }
}
