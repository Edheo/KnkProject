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
            AddTagInfo("Title", $"{_Movie.Title}", "Original Title", $"{_Movie.OriginalTitle}");
            AddTagInfo("Saga", $"{_Movie.MovieSet}", "Duration", _Movie.Extender.Duration()?.ToString(@"hh\:mm\:ss"));
            AddTagInfo();
            AddTagInfo("Year", $"{_Movie.Year}", "Release Date", $"{_Movie.ReleaseDate:dd/MM/yyyy}");
            AddTagInfo("Date Added", $"{_Movie.CreationDate:dd/MM/yyyy}", "Modified", $"{_Movie.ModifiedDate:dd/MM/yyyy}");
            AddTagInfo("Date Scraped", $"{_Movie.ScrapedDate:dd/MM/yyyy}");
            AddTagInfo();
            if (_Movie.ViewedTimes > 0)
            {
                AddTagInfo("Last Played", $"{_Movie.LastViewed:dd/MM/yyyy}", "Plays", $"{_Movie.ViewedTimes}");
            }
            AddTagInfo();
            AddTagInfo("Genre", $"{_Movie.Extender.Genres}");
            AddTagInfo("Country", $"{_Movie.Extender.Countries}");
            AddTagInfo("MPA Rating", $"{_Movie.MPARating}", "Adult Content", $"{_Movie.AdultContent}");
            AddTagInfo();
            AddTagInfo("HomePage", $"{_Movie.HomePage}");
            AddTagInfo("Imdb", $"{_Movie.Extender.ImdbUrl()}");
            AddTagInfo("Tmdb", $"{_Movie.Extender.TmdbUrl()}");
            AddTagInfo();
            AddTagInfo("Votes", $"{_Movie.Votes}", "Rating", $"{_Movie.Rating:0.0}");
            AddTagInfo("Popularity", $"{_Movie.Popularity:0.0}");
            AddTagInfo("User Rating", $"{_Movie.Extender.AveragedRate:0.0}", "Computed Rating", $"{_Movie.Extender.AveragedRate:0.0}");
            AddTagInfo();
            AddTagInfo("Budget", $"{_Movie.Budget:n}", "Revenue", $"{_Movie.Revenue:n}");
            AddTagInfo();
            AddTagInfo("Tag Line", $"{_Movie.TagLine}");
            AddTagInfo();
            //AddTagInfo();
            //AddTagInfo("Director", $"{_Movie.Extender.Director()}", "Writer", $"{_Movie.Extender.Writer()}");
            //AddTagInfo();
            //AddTagInfo(FontStyle.Bold, "Role in Movie", "Artist Name");
            //foreach (var lCast in _Movie.Extender.ArtistCasting())
            //{
            //    AddTagInfo(lCast.Role, lCast.Casting.ArtistName);
            //}

            //AddTagInfo();

            txtSummary.Text = _Movie.Extender.Summary;

            AddCasting();
        }

        public Movie Movie
        {
            get { return _Movie; }
        }

        private void AddCasting()
        {
            var lTypes = _Movie.Casting().Items.GroupBy(typ => typ.IdCastingType.Value).Select(grp => grp.First());
            Control lPrevious = null;
            foreach (var typ in lTypes)
            {
                if (lPrevious!=null)
                {
                    floCrew.SetFlowBreak(lPrevious, true);
                }

                Label lLbl = new Label() { Text = typ.IdCastingType.Reference.Type };
                lLbl.AutoSize = true;
                lLbl.Font = new Font("Verdana", 12, FontStyle.Underline);
                lLbl.ForeColor = Color.LightSkyBlue;
                lLbl.Padding = new Padding(10);
                floCrew.Controls.Add(lLbl);

                var lCast = _Movie.Casting().Items.Where(itm => itm.IdCastingType.Value == typ.IdCastingType.Value);
                foreach (var cst in lCast)
                {
                    if (cst.IdCasting.Reference.Extender.Poster != null)
                    {
                        MovieThumb lTmb = new MovieThumb(cst, 100);
                        floCrew.Controls.Add(lTmb);
                        lPrevious = lTmb;
                    }
                    else
                    {
                        lLbl = new Label() { Text = cst.ToString() };
                        lLbl.AutoSize = true;
                        lLbl.ForeColor = Color.White;
                        lLbl.Padding = new Padding(10);
                        floCrew.Controls.Add(lLbl);
                        lPrevious = lLbl;
                    }
                }
            }
        }

        private void AddTagInfo()
        {
            AddTagInfo(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private void AddTagInfo(string aLabel1, string aContent1)
        {
            AddTagInfo(aLabel1, aContent1, string.Empty, string.Empty);
        }

        private void AddTagInfo(FontStyle aFontStyle, string aLabel1, string aContent1)
        {
            AddTagInfo(aFontStyle, aLabel1, aContent1, string.Empty, string.Empty);
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
            
            tblPanel.Controls.Add(lblLabel1);
            tblPanel.Controls.Add(lblContent1);
            if (!string.IsNullOrWhiteSpace(aContent2))
            {
                Label lblLabel2 = CreateLabelTag(!string.IsNullOrWhiteSpace(aContent2) ? aLabel2 : string.Empty, aFontStyle);
                Label lblContent2 = CreateLabelContent(aContent2, aFontStyle);
                tblPanel.Controls.Add(lblLabel2);
                tblPanel.Controls.Add(lblContent2);
            }
            else
            {
                tblPanel.SetColumnSpan(lblContent1, 3);
            }
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

        private void metroTabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
