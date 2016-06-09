using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KnkInterfaces.Enumerations;
using KnkDataSqlServer.Utilities;
using System;

namespace KnkDataSqlServer.Connection
{
    public class KnkSqlConnection:KnkDataItf
    {
        private SqlConnection _Connection;

        public KnkSqlConnection(KnkConfigurationItf aConf)
        {
            _Connection = KnkSqlServer.ConnectionBuilder(aConf.ServerPath, aConf.Database, aConf.User, aConf.Password);
        }

        public bool Connect()
        {
            _Connection.Open();
            return true;
        }

        public DataTable GetData<T>()
            where T : KnkItemItf, new()
        {
            return KnkSqlServer.GetData(KnkSqlServer.GetCommand<T, T>(_Connection, null));
        }

        public DataTable GetData<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            return KnkSqlServer.GetData(_Connection, aCriteria);
        }

        public DataTable GetListIds<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            return KnkSqlServer.GetListIds(_Connection, aCriteria);
        }

        public string SaveData<T>(T aItem) where T : KnkItemItf, new()
        {
            string lCommand = string.Empty;
            string lReturn = string.Empty;
            switch(aItem.Status())
            {
                case UpdateStatusEnu.Delete:
                    lCommand = KnkSqlServer.GetDynamicDelete(_Connection, aItem);
                    lReturn = "Database Deleted";
                    break;
                case UpdateStatusEnu.Update:
                    lCommand = KnkSqlServer.GetDynamicUpdate(_Connection, aItem);
                    lReturn = "Database Updated";
                    break;
                case UpdateStatusEnu.New:
                    lCommand = KnkSqlServer.GetDynamicInsert(_Connection, aItem);
                    lReturn = "Database Added";
                    break;
            }
            DataTable lTbl = KnkSqlServer.GetData(KnkSqlServer.GetCommand(_Connection, aItem, lCommand));
            if (aItem.Status() != UpdateStatusEnu.Delete && (lTbl?.Rows?.Count??0) > 0)
            {
                aItem.PropertySet(aItem.PrimaryKey(), lTbl.Rows[0][0]);
                DataRow lRow = lTbl.Rows[0];
                var lCon = aItem.Connection();
                lCon.ReadItem(aItem);
            }
            return lReturn;
        }


    }
}
