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
    static class KnkUtility
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

        public static CryptoStream ToCryptoStream(string aFile)
        {
            Stream lFileStream = new FileStream(aFile, FileMode.OpenOrCreate, FileAccess.Write);
            return new CryptoStream(lFileStream, Crypto(aFile).CreateEncryptor(), CryptoStreamMode.Write);
        }

        public static CryptoStream FromCryptoStream(string aFile)
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

        public static string ConfigFilename(string aExt)
        {
            string lCodeBase = Assembly.GetEntryAssembly().CodeBase;
            string[] lSplit = lCodeBase.Split('.');
            lSplit[lSplit.Length - 1] = aExt;
            return new Uri(string.Join(".", lSplit)).LocalPath;
        }

        private static string AppName()
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

        public static string AppName(string aFile)
        {
            string[] lNames = Path.GetFileName(aFile).Split('.');
            Array.Resize(ref lNames, lNames.Length - 1);
            return string.Join(".", lNames);
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string JoinParameters(List<KnkParameterItf> aParameters)
        {
            string lRet = string.Empty;
            string lConditions = string.Empty;
            var lPars = aParameters.Select(p => new { Condition = p.ToSqlWhere(), Connector = KnkUtility.GetEnumDescription(p.Connector) });
            string lBlank = " ";
            var lParAnt = lPars.FirstOrDefault();
            foreach(var lParCur in lPars )
            {
                if (lConditions.Length > 0)
                    lConditions += lBlank + lParAnt.Connector + lBlank;

                lConditions += lParCur.Condition;
                lParAnt = lParCur;
            }
            if (lConditions.Length > 0)
                lRet = "(" + lConditions + ")";
            return lRet;
        }
    }
}
