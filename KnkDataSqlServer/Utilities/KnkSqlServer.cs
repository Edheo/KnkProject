using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkDataSqlServer.Utilities
{
    static class KnkSqlServer
    {
        internal static string GetDynamicSelect<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            return GetDynamicSelect(aCriteria, false);
        }

        internal static string GetDynamicSelect<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria, bool aDistinct)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            string lTable = aCriteria.EntityTable().SourceTable;
            var lSel = string.Empty;
            if (aDistinct)
            {
                string lKey = aCriteria.EntityRelation().RelatedKey;
                lSel = $"Select Distinct [{lKey}] From [{lTable}]";
            }
            else
                lSel = GetSimpleTableSelect(lTable);
            if (aCriteria != null) lSel += GetWhereFromParameters(aCriteria);
            return lSel;
        }

        internal static string GetSimpleTableSelect(string aTable)
        {
            return $"Select * From [{aTable}]";
        }

        internal static string GetWhereFromParameters<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            string lWhere = KnkInterfacesUtils.JoinParameters(aCriteria.GetParameters());
            return lWhere.Length > 0 ? " Where " + lWhere : string.Empty;
        }

        internal static string GetDynamicInsert<T>(T aItem, DataColumnCollection aCols )
            where T : KnkItemItf
        {
            string lInsertTable = $"Insert Into [{aItem.SourceEntity.SourceTable}";
            var lProperties = from prp in KnkInterfacesUtils.GetProperties<KnkItemItf>(aItem)
                join fld in aCols.Cast<DataColumn>()
                on prp.Name.ToLower() equals fld.ColumnName.ToLower() 
                select prp.Name;
            string lInsertFields = lProperties.Aggregate((i, j) => $"[{i}] , [{j}]");
            string lInsertValues = lProperties.Aggregate((i, j) => $"@{i} , @{j}");

            return lInsertTable;
        }
    }
}
