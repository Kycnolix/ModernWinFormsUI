using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwDataGridView : DataGridView
{
    private Color _headerBackColor = Color.White;
    private Color _headerForeColor = MwColors.TextPrimary;
    private Color _rowBackColor = Color.White;
    private Color _alternateRowBackColor = Color.FromArgb(248, 250, 252);
    private Color _rowForeColor = MwColors.TextPrimary;
    private Color _gridLineColor = Color.FromArgb(226, 232, 240);
    private Color _selectionBackColor = Color.FromArgb(239, 246, 255);
    private Color _selectionForeColor = MwColors.TextPrimary;
    private Color _outerBorderColor = Color.FromArgb(226, 232, 240);
    private Color _emptyBackColor = Color.White;

    private int _rowHeightValue = 50;
    private int _headerHeightValue = 44;
    private int _radius = 8;

    [Category("ModernWinFormsUI")]
    [Description("Header background color.")]
    public Color HeaderBackColor
    {
        get => _headerBackColor;
        set
        {
            _headerBackColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Header text color.")]
    public Color HeaderForeColor
    {
        get => _headerForeColor;
        set
        {
            _headerForeColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Default row background color.")]
    public Color RowBackColor
    {
        get => _rowBackColor;
        set
        {
            _rowBackColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Alternating row background color.")]
    public Color AlternateRowBackColor
    {
        get => _alternateRowBackColor;
        set
        {
            _alternateRowBackColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Row text color.")]
    public Color RowForeColor
    {
        get => _rowForeColor;
        set
        {
            _rowForeColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Grid line color.")]
    public Color GridLineColor
    {
        get => _gridLineColor;
        set
        {
            _gridLineColor = value;
            GridColor = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Selected row background color.")]
    public Color SelectionBackColor
    {
        get => _selectionBackColor;
        set
        {
            _selectionBackColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Selected row text color.")]
    public Color SelectionForeColor
    {
        get => _selectionForeColor;
        set
        {
            _selectionForeColor = value;
            ApplyModernStyle();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Outer border color.")]
    public Color OuterBorderColor
    {
        get => _outerBorderColor;
        set
        {
            _outerBorderColor = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Background color of the empty grid area.")]
    public Color EmptyBackColor
    {
        get => _emptyBackColor;
        set
        {
            _emptyBackColor = value;
            BackgroundColor = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Default row height.")]
    [DefaultValue(50)]
    public int ModernRowHeight
    {
        get => _rowHeightValue;
        set
        {
            _rowHeightValue = ClampInt(value, 28, 96);
            RowTemplate.Height = _rowHeightValue;

            foreach (DataGridViewRow row in Rows)
                row.Height = _rowHeightValue;

            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Column header height.")]
    [DefaultValue(44)]
    public int ModernHeaderHeight
    {
        get => _headerHeightValue;
        set
        {
            _headerHeightValue = ClampInt(value, 28, 80);
            ColumnHeadersHeight = _headerHeightValue;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Outer corner radius.")]
    [DefaultValue(8)]
    public int Radius
    {
        get => _radius;
        set
        {
            _radius = ClampInt(value, 0, 24);
            ApplyRoundedRegion();
            Invalidate();
        }
    }

    public MwDataGridView()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw,
            true
        );

        ApplyModernDefaults();
        ApplyModernStyle();
        ApplyRoundedRegion();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        ApplyRoundedRegion();
    }

    protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
    {
        base.OnRowsAdded(e);

        for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
        {
            if (i >= 0 && i < Rows.Count)
                Rows[i].Height = ModernRowHeight;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (Radius <= 0)
        {
            using var pen = new Pen(OuterBorderColor, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            return;
        }

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using var path = GraphicsHelper.CreateRoundedRectangle(rect, Radius);
        using var penRounded = new Pen(OuterBorderColor, 1);

        e.Graphics.DrawPath(penRounded, path);
    }

    private void ApplyModernDefaults()
    {
        BorderStyle = BorderStyle.None;
        BackgroundColor = EmptyBackColor;

        EnableHeadersVisualStyles = false;
        AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        AllowUserToResizeRows = false;

        RowHeadersVisible = false;
        MultiSelect = false;
        SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

        GridColor = GridLineColor;

        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        ColumnHeadersHeight = ModernHeaderHeight;

        RowTemplate.Height = ModernRowHeight;

        ReadOnly = true;
    }

    private void ApplyModernStyle()
    {
        Font = MwFonts.Body;

        ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColor;
        ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColor;
        ColumnHeadersDefaultCellStyle.Font = MwFonts.BodyBold;
        ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        ColumnHeadersDefaultCellStyle.Padding = new Padding(14, 0, 14, 0);
        ColumnHeadersDefaultCellStyle.SelectionBackColor = HeaderBackColor;
        ColumnHeadersDefaultCellStyle.SelectionForeColor = HeaderForeColor;

        DefaultCellStyle.BackColor = RowBackColor;
        DefaultCellStyle.ForeColor = RowForeColor;
        DefaultCellStyle.Font = MwFonts.Body;
        DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        DefaultCellStyle.Padding = new Padding(14, 0, 14, 0);
        DefaultCellStyle.SelectionBackColor = SelectionBackColor;
        DefaultCellStyle.SelectionForeColor = SelectionForeColor;

        AlternatingRowsDefaultCellStyle.BackColor = AlternateRowBackColor;
        AlternatingRowsDefaultCellStyle.ForeColor = RowForeColor;
        AlternatingRowsDefaultCellStyle.SelectionBackColor = SelectionBackColor;
        AlternatingRowsDefaultCellStyle.SelectionForeColor = SelectionForeColor;

        RowsDefaultCellStyle.BackColor = RowBackColor;
        RowsDefaultCellStyle.ForeColor = RowForeColor;
        RowsDefaultCellStyle.SelectionBackColor = SelectionBackColor;
        RowsDefaultCellStyle.SelectionForeColor = SelectionForeColor;

        GridColor = GridLineColor;
        BackgroundColor = EmptyBackColor;
    }

    private void ApplyRoundedRegion()
    {
        if (Width <= 0 || Height <= 0)
            return;

        if (Radius <= 0)
        {
            Region = null;
            return;
        }

        var rect = new Rectangle(0, 0, Width, Height);

        using var path = GraphicsHelper.CreateRoundedRectangle(rect, Radius);
        Region = new Region(path);
    }

    private static int ClampInt(int value, int min, int max)
    {
        if (value < min)
            return min;

        if (value > max)
            return max;

        return value;
    }
}