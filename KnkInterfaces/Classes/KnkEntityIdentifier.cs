using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Classes
{
    public class KnkEntityIdentifier : KnkEntityIdentifierItf
    {
        public KnkEntityIdentifier():this(null)
        {
        }

        public KnkEntityIdentifier(int? aValue)
        {
            SetInnerValue(aValue);
        }
        //KnkEntityIdentifierItf
        private int? _value;

        public int? GetInnerValue()
        {
            return _value;
        }

        public void SetInnerValue(int? aValue)
        {
            _value = aValue;
        }

        public override string ToString()
        {
            return GetInnerValue()?.ToString();
        }

        public static implicit operator KnkEntityIdentifier(int value)
        {
            return new KnkEntityIdentifier(value);
        }

        public static implicit operator int(KnkEntityIdentifier value)
        {
            return (int)value._value;
        }

        public static implicit operator KnkEntityIdentifier(int? value)
        {
            return new KnkEntityIdentifier() { _value = value };
        }

        public static implicit operator int? (KnkEntityIdentifier value)
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
