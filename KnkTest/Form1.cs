using KnkCore;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Movies lLst = new Movies(new KnkConnection());

            //ssageBox.Show(lLst.Items.Count().ToString());

            var lLst2 = (from m in lLst.Items where m.IdSet > 0 select m);
            var lMov1 = lLst2.LastOrDefault();
            var lTst = lMov1;
            if (lTst != null)
            {
                var lMethods = KnkInterfacesUtils.GetMethods(lTst);
                var lAux = (from m in lMethods where m.ReturnType.GenericTypeArguments.Count().Equals(2) && m.ReflectedType==lTst.GetType() select m);
                var lAus = (from m in lAux where m.ReturnType.GetInterfaces().Contains(typeof(KnkListItf)) select m);

                var lMethod = lAus.FirstOrDefault();

                //Type lArgument = (from arg in lMethod.ReturnType.GenericTypeArguments where arg != lMov1.GetType() select arg).FirstOrDefault();
                var lResult = lMethod.Invoke(lMov1, null) as KnkListItf;
                if (lResult != null)
                {
                    lResult.SaveChanges();
                }

            }

            //MessageBox.Show(lMov1.Title);
            //MessageBox.Show(lMov1.MovieSet.Name);

            //MessageBox.Show(lMov1.Files().Count().ToString());
            //MessageBox.Show(lMov1.Extender.Genres.Count().ToString());

            //var lAux = (from m in lMov1.Casting().Items select m).ToList();

            //int i = 0;
            //var lRes = lAux.TakeWhile(m => 5 < i++);

            //var lTst = lAux.LastOrDefault();


            //var lTsx = lTst.CastingType.Type;


            //var ss = lTsx;
        }
    }
}
