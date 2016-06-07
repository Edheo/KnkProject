using KnkCore.Utilities;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkCore
{
    public class KnkEntityIdentifier : KnkEntityIdentifierItf
    {
        private int? _value;

        public KnkEntityIdentifier():this(null)
        {
        }

        public KnkEntityIdentifier(int? aValue)
        {
            Value = aValue;
        }

        public virtual int? Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        public override string ToString()
        {
            return Value?.ToString();
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
            return Convert.ToBoolean(_value ?? 0);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_value ?? 0);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_value ?? 0);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_value ?? 0);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_value ?? 0);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_value ?? 0);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return _value ?? 0;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_value ?? 0);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return _value ?? 0;
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_value ?? 0);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return _value ?? 0;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return _value ?? 0;
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return _value ?? 0;
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
            return _value;
        }

        public int CompareTo(object obj)
        {
            int? lObj = KnkCoreUtils.ObjectToKnkInt(obj);

            if (lObj == this.Value)
                return 0;
            else 
                return 1;
        }

    }
}
