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
        KnkConfiguration _Config = new KnkConfiguration();

        public KnkConnection(bool aTest)
        {
        }

        public KnkConnection():this(false)
        {
        }

        public KnkConfiguration Configuration()
        {
            return _Config;
        }

        KnkDataItf GetConnection(Type aType)
        {
            KnkConfigurer lConf = _Config.CallerConfiguration(aType) as KnkConfigurer;
            return lConf.CreateConnection();
        }

        public KnkListItf<T> GetList<T>() where T : KnkItemItf, new()
        {
            var type = typeof(T);
            KnkListItf<T> lLst = new KnkList<T>(this);
            using (var lDat = GetConnection(typeof(T)).GetData<T>())
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


            using (var lDat = GetConnection(typeof(T)).GetData<T,T>(lCri))
            {
                lLst.FillFromDataTable(lDat);
            }
            return lLst.Items.FirstOrDefault();
        }

        public KnkReferenceItf<TDad, TReference> GetReference<TDad, TReference>(TDad aItem, string aProperty)
            where TDad : KnkItemItf
            where TReference : KnkItemItf, new()
        {
            return SetReference<TDad, TReference>(new KnkReference<TDad, TReference>(aItem, aProperty, GetItem<TReference>),aItem, aProperty);
        }

        public KnkReferenceItf<TDad, TReference> SetReference<TDad, TReference>(KnkReferenceItf<TDad, TReference> aReference, TDad aItem, string aProperty)
            where TDad : KnkItemItf
            where TReference : KnkItemItf, new()
        {
            aReference.ResetReference(aItem, aProperty);
            return aReference;
        }


        public KnkListItf<Tlst> GetList<Tdad, Tlst>(KnkCriteriaItf<Tdad,Tlst> aCriteria) 
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            KnkListItf<Tlst> lLst = new KnkList<Tlst>(this);
            using (var lDat = GetConnection(typeof(Tlst)).GetData<Tdad, Tlst>(aCriteria))
            {
                lLst = FillList<Tdad, Tlst>(lLst, aCriteria);
            };
            return lLst;
        }

        public KnkListItf<T> FillList<T>(KnkListItf<T> aList) where T : KnkItemItf, new()
        {
            var type = typeof(T);
            var lLst = aList;
            using (var lDat = GetConnection(typeof(T)).GetData<T>())
            {
                aList.FillFromDataTable(lDat);
            }
            return lLst;

        }

        public KnkListItf<Tlst> FillList<Tdad, Tlst>(KnkListItf<Tlst> aList, KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            using (var lDat = GetConnection(typeof(Tlst)).GetData<Tdad, Tlst>(aCriteria))
            {
                aList.FillFromDataTable(lDat);
            };
            return aList;
        }
    }
}
