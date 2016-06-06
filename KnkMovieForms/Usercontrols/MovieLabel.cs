using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using KnkMovieForms.Utilities;
using System.Threading;

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieLabel : UserControl
    {
        delegate void delNoParams();
        private string _Text;
        private string _FontName = "Verdana";
        private FontStyle _FontstyleCaption = FontStyle.Bold;
        private FontStyle _FontstyleText = FontStyle.Regular;
        private Color _FontColorCaption = Color.White;
        private Color _FontColorText = Color.White;
        private int? _FontSizeCaption;
        private int? _FontSizeText;
        private string _Caption;

        private bool _horizontal;

        public MovieLabel()
        {
            InitializeComponent();
        }

        [Description("TextAlignment"), Category("Data")]
        public string FontName { get { return _FontName; } set { _FontName = value; } }
        public FontStyle FontstyleCaption { get { return _FontstyleCaption; } set { _FontstyleCaption = value; } }
        public FontStyle FontstyleText { get { return _FontstyleText; } set { _FontstyleText = value; } }
        public bool Horizontal { get { return _horizontal; } set { _horizontal = value; } }
        [Description("Caption"), Category("Data")]
        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
                if (InvokeRequired)
                    this.Invoke(new delNoParams(this.Refresh));
                else
                    Refresh();

            }
        }

        public new string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                if (InvokeRequired)
                    this.Invoke(new delNoParams(this.Refresh));
                else
                    Refresh();
            }
        }

        public void SetValues(string aCaption, string aText)
        {
            _Caption = aCaption;
            _Text = aText;
        }

        public Color FontColorCaption { get { return _FontColorCaption; } set { _FontColorCaption = value; } }
        public Color FontColorText { get { return _FontColorText; } set { _FontColorText = value; } }
        public int? FontSizeCaption { get { return _FontSizeCaption; } set { _FontSizeCaption = value; } }
        public int? FontSizeText { get { return _FontSizeText; } set { _FontSizeText = value; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintCenteredText(e.Graphics);
        }

        private void PaintCenteredText(Graphics aGraphics)
        {
            TextFormatFlags lFormats = TextFormatFlags.WordBreak | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix;
            Rectangle lRectangle = ClientRectangle;
            if (!string.IsNullOrEmpty(Caption))
            {
                using (var lFontBase = new Font(FontName, 10, this.FontstyleCaption))
                {
                    var lSize = aGraphics.MeasureString(Caption, lFontBase);
                    if (_FontSizeCaption == null)
                    {
                        float lFontScale = 1;
                        if (!Horizontal)
                        {
                            if (lSize.Width > lRectangle.Width)
                                lFontScale = lSize.Width / lRectangle.Width;
                        }
                        else
                        {
                            lFontScale = lSize.Height / lRectangle.Height;
                            lRectangle = new Rectangle(0, 0, (int)(lSize.Width / lFontScale), lRectangle.Height);
                        }
                        if (lFontScale > 1.3F) lFontScale = 1.3F;

                        using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.FontstyleCaption, GraphicsUnit.Point))
                        {
                            TextRenderer.DrawText(aGraphics, Caption, lFont, lRectangle, FontColorCaption, lFormats);
                        }
                    }
                    else
                    {
                        lRectangle = new Rectangle(0, 0, (int)lSize.Width, lRectangle.Height);
                        using (Font lFont = new Font(FontName, (int)_FontSizeCaption, this.FontstyleText))
                        {
                            TextRenderer.DrawText(aGraphics, Caption, lFont, lRectangle, FontColorCaption, lFormats);
                        }
                    }
                    if (!Horizontal)
                    {
                        lRectangle = new Rectangle(0, (int)lSize.Height, ClientRectangle.Width, ClientRectangle.Height - (int)lSize.Height);
                    }
                    else
                    {
                        lRectangle = new Rectangle(lRectangle.Width, 0, (int)(ClientRectangle.Width - lSize.Width), ClientRectangle.Height);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Text))
            {
                if (_FontSizeText == null)
                {
                    using (var lFontBase = new Font(FontName, 10, this.FontstyleText))
                    {
                        var lSize = aGraphics.MeasureString(Text, lFontBase);
                        var lFontScale = Math.Max(lSize.Width / lRectangle.Width, lSize.Height / lRectangle.Height);
                        if (lFontScale > 1.3F) lFontScale = 1.3F;
                        using (Font lFont = new Font(lFontBase.FontFamily, lFontBase.SizeInPoints / lFontScale, this.FontstyleText, GraphicsUnit.Point))
                        {
                            TextRenderer.DrawText(aGraphics, Text, lFont, lRectangle, FontColorText, lFormats);
                        }
                    }
                }
                else
                {
                    using (Font lFont = new Font(FontName, (int)_FontSizeText, this.FontstyleText))
                    {
                        TextRenderer.DrawText(aGraphics, Text, lFont, lRectangle, FontColorText, lFormats);
                    }
                }

            }
        }
    }
}
