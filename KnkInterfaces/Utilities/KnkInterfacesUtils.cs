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

        public static PropertyInfo[] GetProperties<T>() where T : KnkItemItf, new()
        {
            return GetProperties(new T());
        }

        public static PropertyInfo[] GetProperties<T>(T item)
            where T : KnkItemItf
        {
            return (from p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance) where p.CanWrite select p).ToArray();
        }

        public static MethodInfo[] GetMethods<T>(T item)
            where T : KnkItemItf
        {
            return (from p in item.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance) select p).ToArray();
        }

        public static List<MethodInfo> GetMethodsRelations<T>(T item)
            where T : KnkItemItf
        {
            var lReturn = (from m in GetMethods(item) 
                where m.ReturnType.GenericTypeArguments.Count().Equals(2) 
                && m.ReflectedType == item.GetType() 
                && m.ReturnType.GetInterfaces().Contains(typeof(KnkListItf))
                select m);
            return lReturn.ToList();
        }


        public static List<string> CreatedFields()
        {
            return new List<string>() { "usercreatedid", "createddate", "createdtext" };
        }

        public static List<string> ModifiedFields()
        {
            return new List<string>() { "usermodifiedid", "modifieddate", "modifiedtext" };
        }

        public static List<string> DeletedFields()
        {
            return new List<string>() { "deleted", "userdeletedid", "deleteddate", "deletedtext"  };
        }


    }
}
