﻿using KnkCore.Utilities;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KnkCore
{
    public  class KnkConnection:KnkConnectionItf
    {
        KnkConfiguration _Config = new KnkConfiguration();
        KnkItemItf _CurrentUser;

        public KnkConnection(bool aTest)
        {
            if (Utilities.KnkCoreUtils.GlobalConn == null && !aTest) Utilities.KnkCoreUtils.GlobalConn = this;
            if (Utilities.KnkCoreUtils.GlobalConn != null)
                this.Login(KnkCoreUtils.GlobalConn.CurrentUser());
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
            return _Config.GetConnection(aType);
        }

        public T GetItem<T>(int? aEntityId) where T : KnkItemItf, new()
        {
            var type = typeof(T);
            T lItm = new T();
            lItm.PropertySet(lItm.PrimaryKey(), aEntityId);
            return ReadItem(lItm);
        }

        public T ReadItem<T>(T aItm) where T : KnkItemItf, new()
        {
            KnkListItf<T, T> lLst = new KnkList<T, T>(this);
            KnkCriteria<T, T> lCri = KnkCoreUtils.BuildEqualCriteria<T,T>(aItm, aItm.PrimaryKey(), aItm.PropertyGet(aItm.PrimaryKey()));

            FillList<T, T>(lLst, lCri);

            return lLst.Items.FirstOrDefault();
        }


        private void FillFromDataTable<Tdad, Tlst>(KnkListItf<Tdad, Tlst> aList, DataTable aTable)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            aList.FillFromList(aTable.AsEnumerable().Select(row => KnkCoreUtils.CopyRecord<Tlst>(aList, row)).ToList());
        }

        //public KnkReferenceItf<TRef> GetReference<TDad, TRef>(TDad aItem)
        //    where TDad : KnkItemItf
        //    where TRef : KnkItemItf, new()
        //{
        //    return SetReference(new KnkEntityReference<TReference>(aItem, lProperty, GetItem<TReference>), aItem, lProperty);
        //}

        //public KnkReferenceItf<TDad, TReference> GetReference<TDad, TReference>(TDad aItem, string aProperty)
        //    where TDad : KnkItemItf
        //    where TReference : KnkItemItf, new()
        //{
        //    return SetReference(new KnkEntityReference<TDad, TReference>(aItem, aProperty, GetItem<TReference>),aItem, aProperty);
        //}

        //public KnkReferenceItf<TDad, TReference> SetReference<TDad, TReference>(KnkReferenceItf<TDad, TReference> aReference, TDad aItem, string aProperty)
        //    where TDad : KnkItemItf
        //    where TReference : KnkItemItf, new()
        //{
        //    aReference.ResetReference(aItem, aProperty);
        //    return aReference;
        //}

        public KnkListItf<Tdad, Tlst> GetList<Tdad, Tlst>()
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            KnkListItf<Tdad, Tlst> lLst = new KnkList<Tdad, Tlst>(this);
            lLst = FillList(lLst, lLst.GetCriteria());
            return lLst;
        }


        public KnkListItf<Tdad, Tlst> GetList<Tdad, Tlst>(KnkCriteriaItf<Tdad,Tlst> aCriteria) 
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            KnkListItf<Tdad, Tlst> lLst = new KnkList<Tdad, Tlst>(this);
            lLst = FillList(lLst, aCriteria);
            return lLst;
        }

        public List<KnkEntityIdentifierItf> GetListIds<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            var lLst = new List<KnkEntityIdentifierItf>();
            using (var lDat = GetConnection(typeof(Tdad)).GetListIds < Tdad, Tlst>(aCriteria))
            {
                foreach(DataRow lRow in lDat.Rows)
                {
                    KnkEntityIdentifierItf lValue = new KnkEntityIdentifier((int)lRow[0]);
                    lLst.Add(lValue);
                }
            }
            return lLst;
        }

        public KnkListItf<T,T> FillList<T>(KnkListItf<T,T> aList) where T : KnkItemItf, new()
        {
            var type = typeof(T);
            var lLst = aList;
            using (var lDat = GetConnection(typeof(T)).GetData<T>())
            {
                FillFromDataTable(aList, lDat);
            }
            return lLst;
        }

        public KnkListItf<Tdad, Tlst> FillList<Tdad, Tlst>(KnkListItf<Tdad, Tlst> aList)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            return FillList(aList, aList.GetCriteria());
        }

        public KnkListItf<Tdad, Tlst> FillList<Tdad, Tlst>(KnkListItf<Tdad, Tlst> aList, KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            using (var lDat = GetConnection(typeof(Tlst)).GetData(aCriteria))
            {
                FillFromDataTable(aList, lDat);
            };
            return aList;
        }

        public void SaveData<T>(List<T> aItems) where T : KnkItemItf, new()
        {
            var lDat = GetConnection(typeof(T));
            foreach (var lItm in aItems)
            {
                if (lItm.Status() != KnkInterfaces.Enumerations.UpdateStatusEnu.NoChanges)
                {
                    var lType = lItm.Status();
                    DebugerCondition(lItm);
                    lDat.SaveData(lItm);
                    //If Saved check if it has chilrens
                    var lMethods= KnkInterfacesUtils.GetMethodsRelations(lItm);
                    foreach(var lMethod in lMethods)
                    {
                        var lResult = lMethod.Invoke(lItm, null) as KnkListItf;
                        if (lResult != null)
                        {
                            lResult.SaveChanges();
                        }
                        lResult = null;
                    }
                }
            }
        }

        void DebugerCondition(object aItem)
        {
            bool lStop = false;
            KnkItem lItm = aItem as KnkItem;
            if(lItm!=null && lItm.Status()==KnkInterfaces.Enumerations.UpdateStatusEnu.New)
            {
                if (lItm.PrimaryKey().Equals("IdCasting"))
                    lStop = true;
            }
        }

        public KnkItemItf CurrentUser()
        {
            return _CurrentUser;
        }

        public KnkItemItf Login(KnkItemItf aUser)
        {
            _CurrentUser = aUser;
            return CurrentUser();
        }
    }
}
