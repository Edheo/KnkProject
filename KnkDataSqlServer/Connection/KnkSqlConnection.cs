using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KnkDataSqlServer.Connection
{
    public class KnkSqlConnection:KnkDataItf
    {
        private SqlConnection _Connection;

        public KnkSqlConnection(KnkConfigurationItf aConf)
        {
            SqlConnectionStringBuilder lConBui = new SqlConnectionStringBuilder();
            lConBui.DataSource = aConf.ServerPath;
            lConBui.InitialCatalog = aConf.Database;
            if(string.IsNullOrEmpty(aConf.User))
                lConBui.IntegratedSecurity = true;
            else
            {
                lConBui.UserID = aConf.User;
                lConBui.Password = aConf.Password;
            }

            _Connection = new SqlConnection(lConBui.ConnectionString);
        }

        public bool Connect()
        {
            _Connection.Open();
            return true;
        }

        public DataTable GetData<T>()
            where T : KnkItemItf, new()
        {
            DataTable lTbl = null;
            using (DataSet lDts = new DataSet())
            {
                new SqlDataAdapter(GetCommand<T,T>(_Connection, null)).Fill(lDts);
                lTbl = lDts.Tables[0];
            }
            return lTbl;
        }

        public DataTable GetData<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            DataTable lTbl = null;
            using (DataSet lDts = new DataSet())
            {
                new SqlDataAdapter(GetCommand(_Connection, aCriteria)).Fill(lDts);
                lTbl = lDts.Tables[0];
            }
            return lTbl;
        }

        private SqlCommand GetCommand<Tdad, Tlst>(SqlConnection aConnection, KnkCriteriaItf<Tdad, Tlst> aCriteria) 
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            string lCommand = KnkUtility.GetDynamicSelectTable(new Tlst().SourceEntity, aCriteria);
            var lRet = new SqlCommand(lCommand, aConnection);
            if (aCriteria != null)
            {
                foreach (KnkParameterItf lPar in aCriteria.GetParameters())
                {
                    if (lPar.Value != null)
                    {
                        lRet.Parameters.AddWithValue(lPar.Name, lPar.Value);
                    }
                }
            }
            return lRet;
        }
    }
}
