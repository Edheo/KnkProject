﻿using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KnkCore
{
    public class KnkConfiguration
    {
        private List<KnkDataModelerItf> Datamodelers;
        private DataTable Configuration;

        public KnkConfiguration()
        {
            LoadDataModelers();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            using (DataSet lDts = ReadConfig())
            {
                if (lDts.Tables.Count > 0)
                {
                    LoadConfigurationValuesToDatamodelers(lDts.Tables[0]);
                }
                else
                {
                    WriteConfig();
                }
            }
        }

        private void LoadConfigurationValuesToDatamodelers(DataTable aTable)
        {
            Configuration = aTable;
            var lConfItems = aTable.AsEnumerable().Select(row =>
                new KnkConfigurer
                {
                    Name = row.Field<string>("Name"),
                    ConnectionType = (ConnectionTypeEnu)row.Field<int>("ConnectionType"),
                    ServerPath = row.Field<string>("ServerPath"),
                    Database = row.Field<string>("Database"),
                    User = row.Field<string>("User"),
                    Password = row.Field<string>("Password")
                }).ToList();
            foreach(KnkConfigurer lItm in lConfItems)
            {
                UpdateDataModeler(lItm);
            }
        }

        private void UpdateDataModeler(KnkConfigurer aItm)
        {
            var lModeler = Datamodelers.Where(e => e.Name == aItm.Name).FirstOrDefault();
            if(lModeler!=null)
            {
                lModeler.ConnectionType = aItm.ConnectionType;
                lModeler.ServerPath = aItm.ServerPath;
                lModeler.Database = aItm.Database;
                lModeler.User = aItm.User;
                lModeler.Password = aItm.Password;
            }
        }

        public List<KnkConfigurationItf> DataModelerToConfigurer()
        {
            return Datamodelers.Select(row => DataModelToConfigurer(row)).ToList();

        }

        KnkConfigurationItf DataModelToConfigurer(KnkConfigurationItf aItem)
        {
            return new KnkConfigurer
            {
                Name = aItem.Name,
                ConnectionType = aItem.ConnectionType,
                ServerPath = aItem.ServerPath,
                Database = aItem.Database,
                User = aItem.User,
                Password = aItem.Password
            };
        }

        private void LoadDataModelers()
        {
            var lList = new List<Assembly>();

            var lDir = new DirectoryInfo(CurrentDirectory());
            if (lDir.Exists)
            {
                lList.AddRange(lDir.GetFiles("*.dll").Select(file => Assembly.LoadFile(file.FullName)));
            }

            var lTypesList = new List<Type>();

            foreach (var currentAssembly in lList)
                lTypesList.AddRange(currentAssembly.GetTypes());

            var lDatamodelersTypes = lTypesList.FindAll(t => t.GetInterfaces().Contains(typeof(KnkDataModelerItf)));

            Datamodelers = lDatamodelersTypes.ConvertAll(t => Activator.CreateInstance(t) as KnkDataModelerItf);
        }

        private string CurrentDirectory()
        {
            string lCodeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder lUri = new UriBuilder(lCodeBase);
            string lPath = Uri.UnescapeDataString(lUri.Path);
            return Path.GetDirectoryName(lPath);
        }


        private string AppFileName()
        {
            return Utilities.KnkCoreUtils.AppFileName();
        }

        private string ConfigFilename(string aExt)
        {
            return Utilities.KnkCoreUtils.ConfigFilename(aExt);
        }

        private DataSet ReadConfig()
        {
            DataSet lDts = new DataSet();
            string lFile = ConfigFilename("Knk");
            if (File.Exists(lFile))
            {
                Stream lStream = Utilities.KnkCoreUtils.FromCryptoStream(ConfigFilename("Knk"));
                try
                {
                    lDts.ReadXml(lStream);
                }
                finally
                {
                    lStream = null;
                }
            }
            return lDts;
        }

        public void WriteConfig()
        {
            WriteConfig(DataModelerToConfigurer());
        }

        public void WriteConfig(List<KnkConfigurationItf> aList)
        {
            DataSet lDts = new DataSet();
            DataTable lTbl = KnkInterfacesUtils.CreateDataTable<KnkConfigurationItf>(aList);
            lDts.Tables.Add(lTbl);
            using (var lCryp = Utilities.KnkCoreUtils.ToCryptoStream(ConfigFilename("Knk")))
            {
                lDts.WriteXml(lCryp, XmlWriteMode.WriteSchema);
            }
            LoadConfiguration();
        }

        public string GetMediaFolder(Type aType)
        {
            return Utilities.KnkCoreUtils.AppDataFolder(CallerConfiguration(aType)?.MediaFolder); 
        }

        public KnkDataItf GetConnection(Type aType)
        {
            KnkConfigurer lCall = CallerConfiguration(aType);
            return lCall.CreateConnection();
        }

        private KnkConfigurer CallerConfiguration(Type aForType)
        {
            KnkConfigurer lReturn = null;
            lReturn = (from Knk in Datamodelers
                where Knk.Assembly.GetTypes().Contains(aForType)
                select DataModelToConfigurer(Knk)).FirstOrDefault() as KnkConfigurer;
            return lReturn ?? new KnkConfigurer();
        }

        public bool CheckConfiguration()
        {
            bool lRet= !DataModelerToConfigurer().Any(p => string.IsNullOrEmpty(p.ServerPath));
            return lRet;
        }


    }
}
