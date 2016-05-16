using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        private string ConfigFilename(string aExt)
        {
            string lCodeBase = Assembly.GetEntryAssembly().CodeBase;
            string[] lSplit = lCodeBase.Split('.');
            lSplit[lSplit.Length-1] = aExt;
            return new Uri(string.Join(".", lSplit)).LocalPath;
        }

        private DataSet ReadConfig()
        {
            DataSet lDts = new DataSet();
            string lFile = ConfigFilename("Knk");
            if (File.Exists(lFile))
                lDts.ReadXml(lFile);
            return lDts;
        }

        public void WriteConfig()
        {
            WriteConfig(DataModelerToConfigurer());
        }

        public void WriteConfig(List<KnkConfigurationItf> aList)
        {
            DataSet lDts = new DataSet();
            DataTable lTbl = KnkUtility.CreateDataTable<KnkConfigurationItf>(aList);
            lDts.Tables.Add(lTbl);
            lDts.WriteXml(ConfigFilename("Knk"), XmlWriteMode.WriteSchema);
            LoadConfiguration();
        }

        internal KnkConfigurationItf CallerConfiguration()
        {
            KnkConfigurationItf lReturn = null;
            var lAsemblies = (from f in new StackTrace().GetFrames()
                select f.GetMethod().ReflectedType.Assembly).Distinct().ToList();

            foreach (Assembly lAssembly in lAsemblies)
            {
                lReturn = (from Knk in Datamodelers
                    where Knk.Name.Equals(lAssembly.CodeBase)
                    select DataModelToConfigurer(Knk)).FirstOrDefault();
                if (lReturn != null) break;
            }
            return lReturn ?? new KnkConfigurer();
        }

        public bool CheckConfiguration()
        {
            bool lRet= !DataModelerToConfigurer().Any(p => string.IsNullOrEmpty(p.ServerPath));
            return lRet;
        }


    }
}
