using KnkInterfaces.Classes;
using KnkInterfaces.Interfaces;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace KnkInterfaces.Utilities
{
    public static class KnkInterfacesUtils 
    {
        public static T CopyRecord<T>(KnkListItf aOwner, DataRow aRow) where T: KnkItemItf, new()
        {
            //Get Properties
            T lNewItem = new T();
            var lProperties = lNewItem.GetType().GetProperties();
            var lColumns = aRow.Table.Columns.Cast<DataColumn>();
            lNewItem.Parent = aOwner;
            foreach (PropertyInfo lPrp in lProperties)
            {
                //If property is generic list, continue
                if (lPrp.PropertyType.IsGenericType && lPrp.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) continue;
                var lPropertyName = lPrp.Name;
                //Check for dbnull, return null if true, or convert to correct type
                var lCol = (from c in lColumns where c.ColumnName.ToLower().Equals(lPropertyName.ToLower()) select c).FirstOrDefault();
                if(lCol != null)
                {
                    dynamic lValue;
                    if (lPrp.PropertyType.Equals(typeof(KnkEntityIdentifier)))
                    {
                        lValue = (KnkEntityIdentifier)(Convert.IsDBNull(aRow[lCol]) ? null : (int?)Convert.ToInt32(aRow[lCol]));
                    }
                    else
                    {
                        lValue = Convert.IsDBNull(aRow[lCol]) ? null : Convert.ChangeType(aRow[lCol], GetPropertyType(lPrp));
                    }
                    lPrp.SetValue(lNewItem, lValue);
                }
            }
            return lNewItem;
        }

        public static PropertyInfo[] GetProperties<T>() where T : KnkItemItf, new()
        {
            return GetProperties(new T());
        }

        public static PropertyInfo[] GetProperties<T>(T item) 
            where T : KnkItemItf
        {
            return item.GetType().GetProperties();
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

    }
}
