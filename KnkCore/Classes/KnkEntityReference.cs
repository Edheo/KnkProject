using KnkCore.Utilities;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;

namespace KnkCore
{
    public class KnkEntityReference<Tref> : KnkEntityIdentifierItf<Tref>
        where Tref : KnkItemItf, new()
    {
        private Tref _reference;
        private Func<int?, Tref> Load { get; set; }
        private int? _value;

        public KnkEntityReference(int? aValue)
        : this(aValue,(new Tref()).Connection().GetItem<Tref>)
        {
        }

        public KnkEntityReference(Tref aItem)
        : this(aItem, (new Tref()).Connection().GetItem<Tref>)
        {
        }

        public KnkEntityReference(int? aValue, Func<int?, Tref> aLoad)
        {
            Load = aLoad;
            ResetReference(aValue);
        }

        public KnkEntityReference(Tref aItem, Func<int?, Tref> aLoad)
        {
            Load = aLoad;
            ResetReference(aItem);
        }

        public int? GetInnerValue()
        {
            if (_reference != null)
            {
                return _reference.PrimaryKeyValue().GetInnerValue();
            }
            else
                return _value;
        }
        #region member variables


        public Tref Value
        {
            get
            {
                if (_value != null && _reference == null && Load != null) _reference = Load(_value);
                return _reference;
            }
        }
        #endregion

        public void SetInnerValue(int? aValue)
        {
            ResetReference(aValue);
        }

        private void ResetReference(int? aValue)
        {
            Release();
            if (aValue == 1)
                _value = aValue;
            else
                _value = aValue;
        }

        private void ResetReference(Tref aItem)
        {
            Release();
            _reference = aItem;
            _value = (_reference?.PropertyGet(_reference.PrimaryKey()) as KnkEntityIdentifier)?.GetInnerValue();
        }

        private void Release()
        {
            _value = null;
            _reference = default(Tref);
        }

        public override string ToString()
        {
            return Value?.ToString();
        }

        public static implicit operator KnkEntityReference<Tref>(int value)
        {
            Tref lTre = new Tref();
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
            int? lObj = KnkCoreUtils.ObjectToKnkInt(obj);

            if (lObj == this.GetInnerValue())
                return 0;
            else
                return 1;
        }


    }
}
