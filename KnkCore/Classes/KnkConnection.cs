using KnkInterfaces.Interfaces;
using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace KnkCore
{
    public  class KnkConnection:KnkConnectionItf
    {
        KnkDataItf _Conection;
        KnkConfiguration _Config = new KnkConfiguration();

        public KnkConnection(bool aTest)
        {
            if (!aTest)
                CreateConnection();
        }

        public KnkConnection():this(false)
        {
        }

        public KnkConfiguration Configuration()
        {
            return _Config;
        }


        private void CreateConnection()
        {
            KnkConfigurer lConf = _Config.CallerConfiguration() as KnkConfigurer;

            switch (lConf.ConnectionType)
            {
                case ConnectionTypeEnu.SqlServer:
                    _Conection = new KnkDataSqlServer.Connection.KnkSqlConnection(lConf);
                    break;
                default:
                    throw new Exception($"Connection type {lConf.ConnectionType} not implemented");
            }
        }

        public bool Connect()
        {
            return _Conection.Connect();
        }

        public KnkListItf<T> GetList<T>() where T : KnkItemItf, new()
        {
            var type = typeof(T);
            KnkListItf<T> lLst = new KnkList<T>(this);
            using (var lDat = _Conection.GetData<T>())
            {
                lLst = this.FillList<T>(lLst);
            }
            return lLst;
        }

        public T GetItem<T>(int? aEntityId) where T : KnkItemItf, new()
        {
            var type = typeof(T);
            T lItm = new T();
            lItm.PropertySet(lItm.SourceEntity.PrimaryKey, aEntityId);
            KnkListItf<T> lLst = new KnkList<T>(this);
            KnkCriteria<T,T> lCri = new KnkCriteria<T, T>(lItm);
            using (var lDat = _Conection.GetData<T,T>(lCri))
            {
                lLst.FillFromDataTable(lDat);
            }
            return lLst.Items.FirstOrDefault();
        }

        public KnkReferenceItf<T> SetReference<T>(KnkReferenceItf<T> aReference, int? aValue) where T : KnkItemItf, new()
        {
            aReference.ResetReference(aValue);
            return aReference;
        }

        public KnkReferenceItf<T> GetReference<T>(int? aValue) where T : KnkItemItf, new()
        {
            return SetReference<T>(new KnkReference<T>(aValue, GetItem<T>),(int?)aValue);
        }

        public KnkListItf<Tlst> GetList<Tdad, Tlst>(KnkCriteriaItf<Tdad,Tlst> aCriteria) 
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            KnkListItf<Tlst> lLst = new KnkList<Tlst>(this);
            using (var lDat = _Conection.GetData<Tdad, Tlst>(aCriteria))
            {
                lLst = FillList<Tdad, Tlst>(lLst, aCriteria);
            };
            return lLst;
        }

        public KnkListItf<T> FillList<T>(KnkListItf<T> aList) where T : KnkItemItf, new()
        {
            var type = typeof(T);
            var lLst = aList;
            using (var lDat = _Conection.GetData<T>())
            {
                aList.FillFromDataTable(lDat);
            }
            return lLst;

        }

        public KnkListItf<Tlst> FillList<Tdad, Tlst>(KnkListItf<Tlst> aList, KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            using (var lDat = _Conection.GetData<Tdad, Tlst>(aCriteria))
            {
                aList.FillFromDataTable(lDat);
            };
            return aList;
        }
    }
}
