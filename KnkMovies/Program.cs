using KnkSolutionUsers.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnkMovies
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            KnkForms.Utilities.KnkFormsUtils.CheckConfiguration();
            Users lUsr = new Users();
            lUsr.Connection.Login(lUsr.Items.FirstOrDefault());
            Application.Run(new Form1());
        }
    }
}
