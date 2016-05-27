using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnkSolutionMovies.Entities;

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieControl : UserControl
    {
        public event EventHandler Close;

        private Movie _Movie;

        public MovieControl(Movie aMovie)
        {
            InitializeComponent();
            picPoster.Factor(new Size(200, 310));
            SetMovie(aMovie);
        }

        private void SetMovie(Movie aMovie)
        {
            _Movie = aMovie;
            tblPanel.RowStyles.Clear();
            picPoster.Filename = _Movie.Extender.Poster?.Extender.GetFileName();
            if (!string.IsNullOrEmpty(_Movie.MovieStet))
                AddTagInfo("Saga", $"{_Movie.MovieStet}");
            AddTagInfo("Title", $"{_Movie.Title}", "Original Title", $"{_Movie.OriginalTitle}");
            AddTagInfo("Year", $"{_Movie.Year}");
            AddTagInfo("Genre", $"{_Movie.Extender.Genres}", "Country", $"{_Movie.Extender.Countries}");
            AddTagInfo("Votes", $"{_Movie.Votes}", "Rating", $"{_Movie.Rating:0.0}");
            AddTagInfo("User Rating", $"{_Movie.UserRating:0.0}", "Computed Rating", $"{_Movie.Extender.AveragedRate:0.0}");

            if (_Movie.Extender.Plays.Count() > 0)
            {
                AddTagInfo("Last Played", $"{_Movie.Extender.LastPlayed():dd/MM/yyyy}", "Plays", $"{_Movie.Extender.Plays.Count()}");
            }

            AddTagInfo();
            AddTagInfo(FontStyle.Bold, "Type", "Artist Name", "Ordinal", "Role in Movie");
            var lCst = (from c in _Movie.Extender.Casting.Items orderby c.Extender.CastingType.Type descending, c.Ordinal select c);
            foreach (var lCast in lCst)
            {
                AddTagInfo(lCast.Extender.CastingType?.ToString(), lCast.ArtistName, lCast.Ordinal.ToString(), lCast.Role);
            }

            AddTagInfo();
            var lPla = (from p in _Movie.Extender.Plays orderby p.DatePlay descending select p);
            if (lPla.Count() > 0)
            {
                AddTagInfo(FontStyle.Bold, "Date", "File", "Time", "Path");
                foreach (var lVie in _Movie.Extender.Plays)
                {
                    AddTagInfo(lVie.Date(), lVie.Filename, lVie.Time(), lVie.Path);
                }
            }

            AddTagInfo();
        }

        public Movie Movie
        {
            get { return _Movie; }
        }

        private void AddTagInfo()
        {
            AddTagInfo(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private void AddTagInfo(string aLabel1, string aContent1)
        {
            AddTagInfo(aLabel1, aContent1, string.Empty, string.Empty);
        }

        private void AddTagInfo(string aLabel1, string aContent1, string aLabel2, string aContent2)
        {
            AddTagInfo(FontStyle.Regular, aLabel1, aContent1, aLabel2, aContent2);
        }

        private void AddTagInfo(FontStyle aFontStyle, string aLabel1, string aContent1, string aLabel2, string aContent2)
        {
            tblPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Label lblLabel1 = CreateLabelTag(!string.IsNullOrWhiteSpace(aContent1) ? aLabel1:string.Empty, aFontStyle);
            Label lblContent1 = CreateLabelContent(aContent1, aFontStyle);
            Label lblLabel2 = CreateLabelTag(!string.IsNullOrWhiteSpace(aContent2) ? aLabel2 : string.Empty, aFontStyle);
            Label lblContent2 = CreateLabelContent(aContent2, aFontStyle);
            tblPanel.Controls.Add(lblLabel1);
            tblPanel.Controls.Add(lblContent1);
            tblPanel.Controls.Add(lblLabel2);
            tblPanel.Controls.Add(lblContent2);
        }

        private Label CreateLabelTag(string aLabel, FontStyle aFontStyle)
        {
            Label lLblLabel = new Label() { Text = aLabel };
            lLblLabel.AutoSize = true;
            lLblLabel.Dock = DockStyle.Fill;
            lLblLabel.Font = new Font("Verdana", 8.25F, aFontStyle, GraphicsUnit.Point, ((byte)(0)));
            lLblLabel.ForeColor = Color.White;
            lLblLabel.TextAlign = ContentAlignment.MiddleRight;
            return lLblLabel;
        }

        private Label CreateLabelContent(string aContent, FontStyle aFontStyle)
        {
            Label lLblLabel = new Label() { Text = aContent };
            lLblLabel.AutoSize = true;
            lLblLabel.Dock = DockStyle.Fill;
            lLblLabel.Font = new Font("Verdana", 8.25F, aFontStyle, GraphicsUnit.Point, ((byte)(0)));
            lLblLabel.ForeColor = Color.White;
            lLblLabel.Location = new Point(92, 0);
            lLblLabel.Name = "lblTitle";
            lLblLabel.Size = new Size(192, 20);
            lLblLabel.TabIndex = 1;
            lLblLabel.TextAlign = ContentAlignment.MiddleLeft;
            return lLblLabel;
        }

        private void OnClose()
        {
            Close?.Invoke(this, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnClose();
        }
    }
}
