using KnkCore;
using KnkInterfaces.Enumerations;
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
            Movies lLst = new Movies();

            MessageBox.Show(lLst.Items.Count().ToString());

            var lLst2 = (from m in lLst.Items where m.IdSet > 0 select m);
            var lMov1 = lLst2.LastOrDefault();

            MessageBox.Show(lMov1.Title);
            MessageBox.Show(lMov1.Extender.MovieSet.Name);

            MessageBox.Show(lMov1.Extender.Files.Count().ToString());
            MessageBox.Show(lMov1.Extender.Genres.Count().ToString());

        }
    }
}
