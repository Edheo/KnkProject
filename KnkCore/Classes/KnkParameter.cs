using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkCore
{
    public class KnkParameter:KnkParameterItf
    {
        public KnkParameter(Type aType, string aName, object aValue)
        {
            Type = aType;
            Name = aName;
            Value = aValue;
        }

        public Type Type { get; }
        public string Name { get; }
        public object Value { get; }

        public string ToSqlWhere()
        {
            string lRet = "[" + Name + "] ";
            if (Value == null)
                lRet += "Is Null ";
            else
                lRet += "= @" + Name + " ";
            return lRet;
        }

    }
}
