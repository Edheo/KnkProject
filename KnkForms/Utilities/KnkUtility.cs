using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using KnkCore;

namespace KnkForms.Utilities
{
    public static class KnkUtility
    {
        public static bool CheckConfiguration()
        {
            KnkConfiguration lCfg = new KnkConnection(true).Configuration();
            while (!lCfg.CheckConfiguration())
            {
                var lFrm = new KnkForms.Windows.ConfigureConnections(lCfg.DataModelerToConfigurer());
                var lDia = lFrm.ShowDialog();
                if (lDia == true)
                {
                    lCfg.WriteConfig(lFrm.Configuration());
                }
                else
                    break;
            }
            return lCfg.CheckConfiguration();
        }
    }
}
