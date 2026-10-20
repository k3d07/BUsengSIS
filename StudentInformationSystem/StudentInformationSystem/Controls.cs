using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BUsengSIS
{
    /// <summary>Panel with a 1px colored border.</summary>
    public class BorderPanel : Panel
    {
        private Color _borderColor = Color.FromArgb(207, 214, 221);

        public BorderPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(_borderColor))
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
    }

    /// <summary>The square "B" logo mark (outlined box with a letter).</summary>
    public class LogoBox : Control
    {
        private int _borderWidth = 1;
        private string _letter = "B";
        private Color _borderColor = Color.White;

        public LogoBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.White;
        }

        public int BorderWidth { get { return _borderWidth; } set { _borderWidth = Math.Max(1, value); Invalidate(); } }
        public string Letter { get { return _letter; } set { _letter = value; Invalidate(); } }
        public Color BorderColor { get { return _borderColor; } set { _borderColor = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int inset = _borderWidth / 2;
            using (var pen = new Pen(_borderColor, _borderWidth))
                e.Graphics.DrawRectangle(pen, inset, inset, Width - _borderWidth, Height - _borderWidth);

            TextRenderer.DrawText(e.Graphics, _letter, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }
    }

    /// <summary>Caption + big number tile used in the statistics strip.</summary>
    public class StatCard : Control
    {
        private static readonly Font CaptionFont = new Font("Segoe UI", 8.25F);
        private static readonly Font ValueFont = new Font("Segoe UI Semibold", 13.5F);
        private string _caption = "";
        private string _value = "0";
        private bool _showLeftBorder = true;

        public StatCard()
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            BackColor = Color.White;
        }

        public string Caption { get { return _caption; } set { _caption = value; Invalidate(); } }
        public string Value { get { return _value; } set { _value = value; Invalidate(); } }
        public bool ShowLeftBorder { get { return _showLeftBorder; } set { _showLeftBorder = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var flags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;
            TextRenderer.DrawText(e.Graphics, _caption, CaptionFont, new Point(16, 8), Color.FromArgb(91, 102, 115), flags);
            TextRenderer.DrawText(e.Graphics, _value, ValueFont, new Point(16, 23), Color.FromArgb(31, 58, 95), flags);
            if (_showLeftBorder)
                using (var pen = new Pen(Color.FromArgb(227, 231, 235)))
                    e.Graphics.DrawLine(pen, 0, 0, 0, Height);
        }
    }

    internal static class NativeMethods
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        /// <summary>Placeholder text for a TextBox (.NET Framework has no PlaceholderText property).</summary>
        public static void SetPlaceholder(TextBox box, string text)
        {
            if (box.IsHandleCreated)
                SendMessage(box.Handle, EM_SETCUEBANNER, (IntPtr)1, text);
            else
                box.HandleCreated += (s, e) => SendMessage(box.Handle, EM_SETCUEBANNER, (IntPtr)1, text);
        }
    }

    internal static class UiTheme
    {
        public static void StyleGrid(DataGridView g)
        {
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.RowHeadersVisible = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.ReadOnly = true;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.GridColor = Color.FromArgb(232, 236, 240);
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;
            g.RowTemplate.Height = 30;

            var head = g.ColumnHeadersDefaultCellStyle;
            head.BackColor = Color.FromArgb(233, 237, 241);
            head.ForeColor = Color.FromArgb(44, 62, 80);
            head.SelectionBackColor = head.BackColor;
            head.SelectionForeColor = head.ForeColor;
            head.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            head.Alignment = DataGridViewContentAlignment.MiddleLeft;
            head.Padding = new Padding(4, 0, 0, 0);

            var cell = g.DefaultCellStyle;
            cell.BackColor = Color.White;
            cell.ForeColor = Color.FromArgb(44, 62, 80);
            cell.SelectionBackColor = Color.FromArgb(220, 232, 245);
            cell.SelectionForeColor = Color.FromArgb(44, 62, 80);
            cell.Font = new Font("Segoe UI", 9F);
            cell.Padding = new Padding(4, 0, 0, 0);
        }
    }
}
