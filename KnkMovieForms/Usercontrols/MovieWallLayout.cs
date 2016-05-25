using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnkSolutionMovies.Lists;
using System.Threading;

namespace KnkMovieForms.Usercontrols
{
    partial class MovieWallLayout : UserControl
    {
        public event CancelEventHandler LoadingData;

        delegate void delNoParams();
        delegate void delMovieThumb(MovieThumb aMovie);

        private Movies _Movies;
        private bool _LoadingMovies;

        public MovieWallLayout()
        {
            InitializeComponent();
            flowMovies.Scroll += (sender, e) => OnScrolling(e.OldValue, e.NewValue);
        }

        public void LoadMovies(Movies aMovies)
        {
            _Movies = aMovies;
            if (InvokeRequired)
                this.Invoke(new delNoParams(ClearMovies));
            else
                ClearMovies();

            if (_Movies != null)
            {
                if (InvokeRequired)
                    this.Invoke(new delNoParams(SetScrollBars));
                else
                    SetScrollBars();
                LoadItems(VisibleItems());
            }
        }

        private void SetScrollBars()
        {
            flowMovies.AutoScroll = LimitItems() < _Movies.Items.Count;
        }

        public int MovieWidth()
        {
            int lMovieWidth = 0;
            if (LimitItems() < _Movies.Items.Count)
            {
                lMovieWidth = MovieControlWidth(System.Windows.Forms.SystemInformation.VerticalScrollBarWidth);
            }
            else
            {
                lMovieWidth = MovieControlWidth();
            }
            return lMovieWidth;
        }

        private void LoadItems(int aTo)
        {
            //this first alwats
            if (_LoadingMovies) return;
            _LoadingMovies = true;

            this.flowMovies.SuspendLayout();
            int c = flowMovies.Controls.Count;
            int i = 0;
            foreach (var lMovie in _Movies.Items)
            {
                if (i > c)
                {
                    MovieThumb lMovieThumb = new MovieThumb(lMovie, MovieWidth());
                    if (InvokeRequired)
                        this.Invoke(new delMovieThumb(AddMovieControl), lMovieThumb);
                    else
                        AddMovieControl(lMovieThumb);
                }
                i++;
                if (i > aTo) break;
            }

            if (InvokeRequired)
                this.Invoke(new delNoParams(ReEnableLayout));
            else
                ReEnableLayout();

            //this last alwats
            _LoadingMovies = false;
        }

        private void ReEnableLayout()
        {
            this.flowMovies.ResumeLayout(true);
        }

        private void ClearMovies()
        {
            flowMovies.Controls.Clear();
        }

        private void AddMovieControl(MovieThumb aMovieThumb)
        {
            flowMovies.Controls.Add(aMovieThumb);
        }

        public int VisibleCols()
        {
            return HowManyColumns(flowMovies.ClientSize.Width);
        }

        public int VisibleRows()
        {
            return HowManyRows(MovieControlHeight());
        }

        int LimitItems()
        {
            return VisibleCols() * (VisibleRows() - 1);
        }

        public int VisibleItems()
        {
            return VisibleCols() * VisibleRows();
        }

        private int MovieControlWidth()
        {
            return MovieControlWidth(0);
        }

        private int MovieControlWidth(int aSubstract)
        {
            int lMyWidth = flowMovies.ClientSize.Width - (aSubstract + 6);
            return (int)Math.Ceiling(lMyWidth / (float)VisibleCols());
        }

        private int MovieControlHeight()
        {
            return MovieThumb.GetHeightFromWidth(MovieControlWidth());
        }

        private int HowManyColumns(int aWdith)
        {
            int lMovieWidth = MovieThumb.NormalSize().Width;
            int lMinWidth = MovieThumb.GetMinimumSize().Width;
            int lReturnValue = (int)Math.Ceiling(aWdith / (float)lMovieWidth);
            float lCheckWidth= aWdith / (float)lReturnValue;
            if (lCheckWidth < lMinWidth && lReturnValue > 1)
                return lReturnValue--;
            return lReturnValue;
        }

        private int HowManyRows(int aHeight)
        {
            return aHeight == 0 ? 0 : (int)Math.Ceiling(flowMovies.ClientSize.Height / (float)aHeight);
        }

        private void OnScrolling(int aOldValue, int aNewValue)
        {
            if (aOldValue < aNewValue && flowMovies.Controls.Count < _Movies.Count())
            {
                int lLast = CurrentLastScrolledRow(aNewValue);
                int lLoad = LoadedRows();
                if (lLast >= lLoad)
                {
                    bool aRefres = (aNewValue == this.flowMovies.VerticalScroll.Maximum);
                    int aItems = flowMovies.Controls.Count + 2 * VisibleCols();
                    var lThr = new Thread(() => LoadItems(aItems));
                    lThr.Start();
                }
                else
                {
                    lLast = lLoad;
                }
            }
        }

        private int LoadedRows()
        {
            var lHowMany = this.flowMovies.Controls.Count / (float)this.VisibleCols();
            return (int)Math.Ceiling(lHowMany);
        }

        private int CurrentLastScrolledRow(int aValue)
        {
            var lCurrent = (aValue + this.flowMovies.ClientSize.Height) / (float)MovieControlHeight();
            return (int)Math.Ceiling(lCurrent);
        }
    }
}
