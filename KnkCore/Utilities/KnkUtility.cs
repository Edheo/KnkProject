using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
    }
}
