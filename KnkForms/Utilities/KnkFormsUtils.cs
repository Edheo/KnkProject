using KnkCore;

namespace KnkForms.Utilities
{
    public static class KnkFormsUtils
    {
        public static bool CheckConfiguration()
        {
            KnkConfiguration lCfg = new KnkConnection(true).Configuration();
            while (!lCfg.CheckConfiguration())
            {
                var lFrm = new global::KnkForms.Windows.ConfigureConnections(lCfg.DataModelerToConfigurer());
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
