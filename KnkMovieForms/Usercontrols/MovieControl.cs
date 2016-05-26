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
            AddTagInfo("Title", $"{_Movie.Title}");
            AddTagInfo("Year", $"{_Movie.Year}");
            picPoster.Filename = _Movie.Extender.Poster?.Extender.GetFileName();
            AddTagInfo("Votes", $"{_Movie.Votes}");
            AddTagInfo("Rating", $"{_Movie.Rating:0.0}");
            if (_Movie.UserRating != null)
            {
                AddTagInfo("User Rating", $"{_Movie.UserRating:0.0}");
                AddTagInfo("Computed Rating", $"{_Movie.Extender.AveragedRate:0.0}");
            }
            if (_Movie.Extender.Plays.Count() > 0)
            {
                AddTagInfo("Last Played", $"{_Movie.Extender.LastPlayed():dd/MM/yyyy}");
                AddTagInfo("Plays", $"{_Movie.Extender.Plays.Count()}");
            }
        }

        private void AddTagInfo(string aLabel, string aContent)
        {
            Label lblLabel = CreateLabelTag(aLabel);
            Label lblContent = CreateLabelContent(aContent);
            tblPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblPanel.Controls.Add(lblLabel);
            tblPanel.Controls.Add(lblContent);
        }

        private Label CreateLabelTag(string aLabel)
        {
            Label lLblLabel = new Label() { Text = aLabel };
            lLblLabel.AutoSize = true;
            lLblLabel.Dock = DockStyle.Fill;
            lLblLabel.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lLblLabel.ForeColor = Color.White;
            lLblLabel.TextAlign = ContentAlignment.MiddleRight;
            return lLblLabel;
        }

        private Label CreateLabelContent(string aContent)
        {
            Label lLblLabel = new Label() { Text = aContent };
            lLblLabel.AutoSize = true;
            lLblLabel.Dock = DockStyle.Fill;
            lLblLabel.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lLblLabel.ForeColor = Color.White;
            lLblLabel.Location = new Point(92, 0);
            lLblLabel.Name = "lblTitle";
            lLblLabel.Size = new Size(192, 20);
            lLblLabel.TabIndex = 1;
            lLblLabel.TextAlign = ContentAlignment.MiddleLeft;
            return lLblLabel;
        }

    }
}
