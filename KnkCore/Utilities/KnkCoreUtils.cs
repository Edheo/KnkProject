using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var lLst = (from e in aList select e.GetInnerValue());
            var lStr = String.Join(",", lLst.ToArray());
            aCriteria.AddParameter(typeof(string), aField, OperatorsEnu.In, lStr);
        }

        public static KnkCriteria<Tdad, Tlst> BuildLikeCriteria<Tdad, Tlst>(string aField, string aValue, string aTable, string aFieldId)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            KnkCriteria<Tdad, Tlst> lCri = new KnkCriteria<Tdad, Tlst>(new Tdad(), new KnkTableEntityRelation<Tdad>(aTable, aFieldId));
            lCri.AddParameter(typeof(string), aField, OperatorsEnu.Like, $"%{aValue}%");
            return lCri;
        }


    }
}
