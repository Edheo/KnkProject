using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkInterfaces.PropertyAtributes;
using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace KnkCore.Utilities
{
    public static class KnkCoreUtils
    {
        public static KnkConnectionItf GlobalConn;

        private static Rijndael Crypto(string aFile)
        {
            Rijndael lCrypto = Rijndael.Create();
            string[] lNames = Path.GetFileName(aFile).Split('.');
            Array.Resize(ref lNames, lNames.Length - 1);
            string lKey = string.Join(".", lNames);
            while (lKey.Length < 16)
                lKey += lKey;
            lKey = lKey.Substring(1, 16);

            lCrypto.IV = ASCIIEncoding.ASCII.GetBytes(lKey);
            lCrypto.Key = ASCIIEncoding.ASCII.GetBytes(GetProcessorId());
            return lCrypto;
        }

        internal static CryptoStream ToCryptoStream(string aFile)
        {
            Stream lFileStream = new FileStream(aFile, FileMode.OpenOrCreate, FileAccess.Write);
            return new CryptoStream(lFileStream, Crypto(aFile).CreateEncryptor(), CryptoStreamMode.Write);
        }

        internal static CryptoStream FromCryptoStream(string aFile)
        {
            Stream lFileStream = new FileStream(aFile, FileMode.Open, FileAccess.Read);
            return new CryptoStream(lFileStream, Crypto(aFile).CreateDecryptor(), CryptoStreamMode.Read);
        }

        static string GetProcessorId()
        {
            string lReturn = string.Empty;
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                lReturn = managObj.Properties["processorID"].Value.ToString();
                break;
            }
            return lReturn;
        }

        public static string AppFileName()
        {
            string lCodeBase = Assembly.GetEntryAssembly().CodeBase;
            string[] lSplit = lCodeBase.Split('.');
            Array.Resize(ref lSplit, lSplit.Length - 1);
            return new Uri(string.Join(".", lSplit)).LocalPath;
        }

        internal static string ConfigFilename(string aExt)
        {
            string lCodeBase = Assembly.GetEntryAssembly().CodeBase;
            string[] lSplit = lCodeBase.Split('.');
            lSplit[lSplit.Length - 1] = aExt;
            return new Uri(string.Join(".", lSplit)).LocalPath;
        }

        static string AppName()
        {
            return AppName(Assembly.GetEntryAssembly().CodeBase);
        }

        public static string AppDataFolder(string aFolder)
        {
            string lPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName());
            if(!string.IsNullOrEmpty(aFolder))
                lPath= lPath = Path.Combine(lPath, aFolder);
            if (!Directory.Exists(lPath))
                Directory.CreateDirectory(lPath);
            return lPath;
        }

        public static string AppDataFolder()
        {
            return AppDataFolder(null);
        }

        static string AppName(string aFile)
        {
            string[] lNames = Path.GetFileName(aFile).Split('.');
            Array.Resize(ref lNames, lNames.Length - 1);
            return string.Join(".", lNames);
        }

        public static void CreateInParameter<Tdad, Tlst>(KnkList<Tdad, Tlst> aList, KnkCriteria<Tdad, Tdad> aCriteria, string aField)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            CreateInParameter<Tdad, Tlst>(aList.GetListIds(), aCriteria, aField);
        }

        public static void CreateInParameter<Tdad, Tlst>(List<KnkEntityIdentifierItf> aList, KnkCriteria<Tdad, Tdad> aCriteria, string aField)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            var lLst = (from e in aList select e.Value);
            var lStr = String.Join(",", lLst.ToArray());
            aCriteria.AddParameter(typeof(string), aField, OperatorsEnu.In, lStr);
        }

        public static KnkCriteria<Tdad, Tlst> BuildLikeCriteria<Tdad, Tlst>(string aField, string aValue, string aTable, string aFieldId)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            return BuildCriteria<Tdad, Tlst>(aField, OperatorsEnu.Like, aValue, aTable, aFieldId);
        }

        public static KnkCriteria<Tdad, Tlst> BuildCriteria<Tdad, Tlst>(string aField, OperatorsEnu aOperator, object aValue, string aTable, string aFieldId)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            KnkCriteria<Tdad, Tlst> lCri = new KnkCriteria<Tdad, Tlst>(new Tdad(), new KnkTableEntityRelation<Tdad>(aTable, aFieldId));
            lCri.AddParameter(typeof(string), aField, aOperator, aValue);
            return lCri;
        }

        public static KnkCriteria<Tdad, Titm> BuildEqualCriteria<Tdad, Titm>(Tdad aItem, string aField, object aValue)
            where Tdad : KnkItemItf, new()
            where Titm : KnkItemItf, new()
        {
            KnkCriteria<Tdad, Titm> lCri = new KnkCriteria<Tdad, Titm>(aItem);
            lCri.AddParameter(typeof(int), aField, OperatorsEnu.Equal, aValue);
            return lCri;
        }

        public static KnkCriteria<Tdad, Tlst> BuildRelationCriteria<Tdad, Tlst>(Tdad aItem, string aTableView)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            var lEnt = new Tlst().SourceEntity();
            KnkCriteria<Tdad, Tlst> lCri = new KnkCriteria<Tdad, Tlst>(aItem, new KnkTableEntityRelation<Tdad>(aTableView, lEnt.TableBase));
            lCri.AddParameter(typeof(string), aItem.PrimaryKey(), OperatorsEnu.Equal, aItem.PropertyGet(aItem.PrimaryKey()));
            return lCri;
        }

        public static Titm CopyRecord<Titm>(KnkListItf aOwner, DataRow aRow)
            where Titm : KnkItemItf, new()
        {
            Titm lNewItem = new Titm();
            lNewItem.SetParent(aOwner);
            return CopyRecord<Titm>(lNewItem, aRow);
        }

        public static Titm CopyRecord<Titm>(Titm aItem, DataRow aRow)
            where Titm : KnkItemItf, new()
        {
            //Get Properties
            var lJoined = from prp in aItem.GetType().GetProperties()
                          join fld in aRow.Table.Columns.Cast<DataColumn>()
                          on prp.Name.ToLower() equals fld.ColumnName.ToLower()
                          select prp;

            foreach (PropertyInfo lPrp in lJoined)
            {
                //If property is generic list, continue
                if (lPrp.PropertyType.IsGenericType && lPrp.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) continue;
                //Check for dbnull, return null if true, or convert to correct type
                dynamic lValue = Convert.IsDBNull(aRow[lPrp.Name]) ? null : ChangeType<Titm>(aRow[lPrp.Name], lPrp);
                lPrp.SetValue(aItem, lValue);
            }
            return aItem;
        }

        private static object ChangeType<Titm>(object aValue, PropertyInfo aPrp)
        {
            var lType = GetPropertyType(aPrp);
            var lIsReference = lType.FullName.Contains("KnkEntityReference");
            if (lIsReference)
                return Activator.CreateInstance(aPrp.PropertyType, (int)aValue);
            if (lType == typeof(KnkEntityIdentifier))
                return new KnkEntityIdentifier((int)aValue);
            else if (lType == typeof(KnkEntityIdentifierItf))
                return new KnkEntityIdentifier((int)aValue);
            else
            {
                return Convert.ChangeType(aValue, lType);
            }

        }

        public static PropertyInfo GetPrimaryKey<T>(T item)
            where T : KnkItemItf
        {
            return (from prp in KnkInterfacesUtils.GetProperties(item) where Attribute.IsDefined(prp, typeof(AtributePrimaryKey)) select prp).FirstOrDefault();
        }

        public static Type GetPropertyType(PropertyInfo aProperty)
        {
            return Nullable.GetUnderlyingType(aProperty.PropertyType) ?? aProperty.PropertyType;
        }

        public static int? ObjectToKnkInt(object aVal)
        {
            if (aVal == null)
                return null;

            KnkEntityIdentifier lVal = aVal as KnkEntityIdentifier;
            if (lVal != null) return lVal.Value;
            try
            {
                int lInt = (int)aVal;
                return lInt;
            }
            catch
            {
                return null;
            }
        }

        public static int? ObjectToKnkInt<Tref>(object aVal)
        where Tref : KnkItemItf, new()
        {
            return ObjectToKnkInt(aVal);
        }

        public static string CleanFileName(string aFileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(aFileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
