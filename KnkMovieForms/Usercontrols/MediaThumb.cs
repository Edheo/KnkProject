﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnkSolutionMovies.Entities;
using System.Threading;
using System.Windows.Media.Imaging;

namespace KnkMovieForms.Usercontrols
{
    public partial class MediaThumb : UserControl
    {
        public ScaleEnu Scale = ScaleEnu.Normal;

        internal MediaThumb()
        {
            InitializeComponent();
        }

        internal MediaThumb(int aWidth):this()
        {
            picPoster.Click += (sender, e) => { this.OnClick(e); };
            picPoster.MouseHover += (sender, e) => { OnRemarkMovie(sender, e); };
            SetSize(aWidth);
        }

        private void OnRemarkMovie(object sender, EventArgs e)
        {
            if (!IsMarked())
            {
                this.Parent.SuspendLayout();
                picPoster.ReMarkMovie(true);
                //Control lContainer = this.Container as Control;
                var controls = from lCtn in Parent.Controls.OfType<MediaThumb>() where lCtn != this && lCtn.IsMarked() select lCtn;
                foreach (var lCtl in controls)
                {
                    lCtl.UnRemarkMovie();
                }
                this.Parent.ResumeLayout();
            }
        }

        public bool Horizontal
        {
            get
            {
                return picVals.Horizontal;
            }
            set
            {
                picVals.Horizontal = value;
            }
        }
        public void UnRemarkMovie()
        {
            picPoster.ReMarkMovie(false);
        }

        public bool IsMarked()
        {
            return picPoster.HasBorder();
        }

        public static Size NormalSize()
        {
            return new Size(200, 310 + 50);
        }

        static float Aspect()
        {
            Size lSiz = NormalSize();
            return (float)lSiz.Height / (float)lSiz.Width;
        }

        public static Size GetSize(float aFactor)
        {
            Size lSiz = NormalSize();
            lSiz.Width = (int)(lSiz.Width * aFactor);
            lSiz.Height = GetHeightFromWidth(lSiz.Width);
            return lSiz;
        }

        public static int GetHeightFromWidth(int aWidth)
        {
            return (int)(aWidth * Aspect());
        }

        private static float ScaleMin(ScaleEnu aScale)
        {
            return ((float)aScale - 1) / (float)aScale;
        }

        private static float ScaleMax(ScaleEnu aScale)
        {
            return ((float)aScale + 1) / (float)aScale;
        }

        public static Size GetMinimumSize(ScaleEnu aScale)
        {
            return GetSize(ScaleMin(aScale));
        }

        public static Size GetMaximumSize(ScaleEnu aScale)
        {
            return GetSize(ScaleMax(aScale));
        }

        public override Size MinimumSize { get { return GetMinimumSize(Scale); } set { base.MinimumSize = GetMinimumSize(Scale); } }

        public override Size MaximumSize { get { return GetMaximumSize(Scale); } set { base.MaximumSize = GetMaximumSize(Scale); } }

        public void SetSize(int aWidth)
        {
            Size = new Size(aWidth, GetHeightFromWidth(aWidth));
        }

        public string FileName
        {
            get
            {
                return picPoster.Filename;
            }
            set
            {
                picPoster.Filename = value;
                if (string.IsNullOrEmpty(picPoster.Filename))
                {
                    this.SuspendLayout();
                    this.Controls.Remove(picPoster);
                    this.picVals.Dock = DockStyle.Fill;
                    this.ResumeLayout(true);
                }
            }
        }

        public void SetValues(string aCaption, string aText)
        {
            picVals.SetValues(aCaption, aText);
            picVals.Invalidate();
        }

    }
}
