using KnkInterfaces.Classes;
using KnkInterfaces.Interfaces;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace KnkInterfaces.Utilities
{
    public static class KnkInterfacesUtils 
    {
        public static Titm CopyRecord<Titm>(KnkListItf aOwner, DataRow aRow)
            where Titm : KnkItemItf, new()
        {
            Titm lNewItem = new Titm();
            lNewItem.SetParent(aOwner);
            return CopyRecord<Titm>(lNewItem, aRow);
        }

        public static Titm CopyRecord<Titm>(Titm aItem, DataRow aRow) 
            where Titm: KnkItemItf, new()
        {
            //Get Properties
            var lJoined = from prp in aItem.GetType().GetProperties()
                          join fld in aRow.Table.Columns.Cast<DataColumn>()
                          on prp.Name.ToLower() equals fld.ColumnName.ToLower()
                          select prp;

            foreach (PropertyInfo lPrp in lJoined)
            {
                //If property is generic list, continue
                if (lPrp.PropertyType.IsGenericType && lPrp.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) continue;
                //Check for dbnull, return null if true, or convert to correct type
                dynamic lValue = Convert.IsDBNull(aRow[lPrp.Name]) ? null : ChangeType<Titm>(aRow[lPrp.Name], lPrp);
                lPrp.SetValue(aItem, lValue);
            }
            return aItem;
        }

        private static object ChangeType<Titm>(object aValue, PropertyInfo aPrp)
        {
            var lType = GetPropertyType(aPrp);
            var lIsReference = lType.FullName.Contains("KnkEntityReference");
            if (lIsReference)
                return Activator.CreateInstance(aPrp.PropertyType, (int)aValue);
            if (lType == typeof(KnkEntityIdentifier))
                return new KnkEntityIdentifier((int)aValue);
            else if (lType == typeof(KnkEntityIdentifierItf))
                return new KnkEntityIdentifier((int)aValue);
            //else if (lType == typeof(KnkEntityReference<Tdad, TReference>))
            //    return new KnkEntityReference<Tdad, TReference>()
            else
            {
                return Convert.ChangeType(aValue, lType);
            }

        }

        public static PropertyInfo[] GetProperties<T>() where T : KnkItemItf, new()
        {
            return GetProperties(new T());
        }

        public static PropertyInfo[] GetProperties<T>(T item) 
            where T : KnkItemItf
        {
            return (from p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance) where p.CanWrite select p).ToArray();
        }

        public static PropertyInfo GetPrimaryKey<T>(T item)
            where T : KnkItemItf
        {
            return (from prp in GetProperties(item) where Attribute.IsDefined(prp, typeof(AtributePrimaryKey)) select prp).FirstOrDefault();
        }

        public static Type GetPropertyType(PropertyInfo aProperty)
        {
            return Nullable.GetUnderlyingType(aProperty.PropertyType) ?? aProperty.PropertyType;
        }


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string JoinParameters(List<KnkParameterItf> aParameters)
        {
            string lRet = string.Empty;
            string lConditions = string.Empty;
            var lPars = aParameters.Select(p => new { Condition = p.ToSqlWhere(), Connector = KnkInterfacesUtils.GetEnumDescription(p.Connector) });
            string lBlank = " ";
            var lParAnt = lPars.FirstOrDefault();
            foreach (var lParCur in lPars)
            {
                if (lConditions.Length > 0)
                    lConditions += lBlank + lParAnt.Connector + lBlank;

                lConditions += lParCur.Condition;
                lParAnt = lParCur;
            }
            if (lConditions.Length > 0)
                lRet = "(" + lConditions + ")";
            return lRet;
        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static int? ObjectToKnkInt(object aVal)
        {
            if (aVal == null)
                return null;

            KnkEntityIdentifier lVal = aVal as KnkEntityIdentifier;
            if (lVal != null)
                return lVal.GetInnerValue();

            try
            {
                int lInt = (int)aVal;
                return lInt;
            }
            catch
            {
                return null;
            }
        }

        public static int? ObjectToKnkInt<Tref>(object aVal)
        where Tref : KnkItemItf, new()
        {
            if (aVal == null)
                return null;

            KnkEntityReference<Tref> lVal = aVal as KnkEntityReference<Tref>;
            if (lVal != null)
                return lVal.GetInnerValue();

            try
            {
                int lInt = (int)aVal;
                return lInt;
            }
            catch
            {
                return null;
            }
        }

        public static List<string> CreatedFields()
        {
            return new List<string>() { "usercreatedid", "createddate" };
        }

        public static List<string> ModifiedFields()
        {
            return new List<string>() { "usermodifiedid", "modifieddate" };
        }

        public static List<string> DeletedFields()
        {
            return new List<string>() { "userdeletedid", "deleteddate", "deleted" };
        }
    }
}
