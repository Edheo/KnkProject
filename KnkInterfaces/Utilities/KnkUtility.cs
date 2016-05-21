using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Utilities
{
    public static class KnkUtility 
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
                    var lValue = Convert.IsDBNull(aRow[lCol]) ? null : Convert.ChangeType(aRow[lCol], GetPropertyType(lPrp));
                    lPrp.SetValue(lNewItem, lValue);
                }
            }
            return lNewItem;
        }

        public static PropertyInfo[] GetProperties<T>() where T : KnkItemItf, new()
        {
            return GetProperties(new T());
        }

        public static PropertyInfo[] GetProperties<T>(T item) where T : KnkItemItf
        {
            return item.GetType().GetProperties();
        }

        public static Type GetPropertyType(PropertyInfo aProperty)
        {
            return Nullable.GetUnderlyingType(aProperty.PropertyType) ?? aProperty.PropertyType;
        }

        public static string GetDynamicSelectTable(KnkTableEntityItf aEntity, string aCriteriaTable)
        {
            return "Select * From [" + (string.IsNullOrEmpty(aCriteriaTable) ? aEntity.SourceTable : aCriteriaTable) + "]";
        }

        public static string GetDynamicSelectTable(KnkTableEntityItf aEntity, KnkCriteriaItf aCriteria)
        {
            var lSel = GetDynamicSelectTable(aEntity, aCriteria?.EntitySource());
            if (aCriteria != null)
                lSel += aCriteria.GetWhereFromParameters();
            return lSel;
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
