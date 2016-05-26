using System;
using System.ComponentModel;
using System.Windows.Forms;
using KnkSolutionMovies.Lists;
using System.Threading;
using System.Drawing;

namespace KnkMovieForms.Usercontrols
{
    partial class MovieWallLayout : UserControl
    {
        public event CancelEventHandler LoadingItems;
        public event CancelEventHandler LoadedItems;

        delegate void delNoParams();
        delegate void delMovieThumb(MovieThumb aMovie);

        private Movies _Movies;
        private bool _LoadingMovies;

        public MovieWallLayout()
        {
            InitializeComponent();
            flowMovies.Scroll += (sender, e) => OnScrolling(e.OldValue, e.NewValue);
            flowMovies.MouseWheel += (sender, e) => OnScrolling(flowMovies.VerticalScroll.Value, flowMovies.VerticalScroll.Value - e.Delta);
            flowMovies.PreviewKeyDown += (sender, e) => MappingKeys(e.KeyCode);
        }

        public void OnLoadingItems()
        {
            LoadingItems?.Invoke(this, new CancelEventArgs());
        }

        public void OnLoadedItems()
        {
            LoadedItems?.Invoke(this, new CancelEventArgs());
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
                lMovieWidth = MovieControlWidth(SystemInformation.VerticalScrollBarWidth);
            }
            else
            {
                lMovieWidth = MovieControlWidth();
            }
            return lMovieWidth;
        }

        public int LoadedMovies()
        {
            return flowMovies.Controls.Count;
        }

        private void LoadItems(int aTo)
        {
            //this first alwats
            if (_LoadingMovies) return;
            _LoadingMovies = true;
            OnLoadingItems();

            this.flowMovies.SuspendLayout();
            int lMovies = LoadedMovies();
            int i = 0;
            foreach (var lMovie in _Movies.Items)
            {
                if (i > lMovies)
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
            OnLoadedItems();
            //this last alwats
            _LoadingMovies = false;
        }

        private void ShowMovie(MovieThumb aMovie)
        {
            this.SuspendLayout();
            MovieControl lCtl = new MovieControl(aMovie.Movie());
            lCtl.Dock = DockStyle.Fill;
            flowMovies.Visible = false;
            this.Controls.Add(lCtl);
            this.ResumeLayout();
        }

        private void ReEnableLayout()
        {
            flowMovies.ResumeLayout(true);
            flowMovies.Refresh();
            flowMovies.Focus();
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.flowMovies.Focus();
        }

        void MappingKeys(Keys aKeyPressed)
        {
            switch (aKeyPressed)
            {
                case Keys.PageUp:
                    if(flowMovies.VerticalScroll.Minimum< flowMovies.VerticalScroll.Value - flowMovies.VerticalScroll.LargeChange)
                        flowMovies.VerticalScroll.Value -= flowMovies.VerticalScroll.LargeChange;
                    else
                        flowMovies.VerticalScroll.Value = flowMovies.VerticalScroll.Minimum;
                    break;
                case Keys.PageDown:
                    var lValue = flowMovies.VerticalScroll.Value;
                    OnScrolling(lValue, lValue + flowMovies.VerticalScroll.LargeChange);
                    if (flowMovies.VerticalScroll.Maximum > flowMovies.VerticalScroll.Value + flowMovies.VerticalScroll.LargeChange)
                        flowMovies.VerticalScroll.Value += flowMovies.VerticalScroll.LargeChange;
                    else
                        flowMovies.VerticalScroll.Value = flowMovies.VerticalScroll.Maximum;
                    break;
            }
        }

        private void OnScrolling(int aOldValue, int aNewValue)
        {
            if (aOldValue < aNewValue && LoadedMovies() <= _Movies.Count())
            {
                int lLast = CurrentLastScrolledRow(aNewValue);
                int lLoad = LoadedRows();
                if (lLast >= lLoad)
                {
                    bool aRefres = (aNewValue == this.flowMovies.VerticalScroll.Maximum);
                    int aItems = LoadedMovies() + 2 * VisibleCols();
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
            var lHowMany = LoadedMovies() / (float)this.VisibleCols();
            return (int)Math.Ceiling(lHowMany);
        }

        private int CurrentLastScrolledRow(int aValue)
        {
            var lCurrent = (aValue + this.flowMovies.ClientSize.Height) / (float)MovieControlHeight();
            return (int)Math.Ceiling(lCurrent);
        }
    }
}
