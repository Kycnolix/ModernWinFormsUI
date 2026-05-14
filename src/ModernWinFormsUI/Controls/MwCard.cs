using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwCard : Panel
{
    private int _radius = MwRadius.Lg;
    private Color _borderColor = MwColors.Border;
    private Color _cardBackColor = MwColors.Surface;
    private string _title = string.Empty;
    private string _subtitle = string.Empty;

    [Category("ModernWinFormsUI")]
    public int Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    public Color CardBackColor
    {
        get => _cardBackColor;
        set
        {
            _cardBackColor = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    public string SubTitle
    {
        get => _subtitle;
        set
        {
            _subtitle = value;
            Invalidate();

        }

    }

    public MwCard()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true
        );

        BackColor = Color.Transparent;

        // Title + Subtitle alanı için üst boşluğu daha büyük tutuyoruz.
        Padding = new Padding(
            MwSpacing.Lg,
            64,
            MwSpacing.Lg,
            MwSpacing.Lg
        );

        Margin = new Padding(MwSpacing.Sm);
    }

    private void DrawTitle(Graphics graphics)
    {
        if (string.IsNullOrWhiteSpace(Title))
            return;

        var titleRect = new Rectangle(
            MwSpacing.Lg,
            MwSpacing.Md,
            Width - MwSpacing.Xl,
            28
        );

        TextRenderer.DrawText(
            graphics,
            Title,
            MwFonts.Subtitle,
            titleRect,
            MwColors.TextPrimary,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }

    private void DrawSubtitle(Graphics graphics)
    {
        if (string.IsNullOrWhiteSpace(SubTitle))
            return;

        var subtitleRect = new Rectangle(
            MwSpacing.Lg,
            MwSpacing.Lg + 22,
            Width - MwSpacing.Xl,
            24
        );

        TextRenderer.DrawText(
            graphics,
            SubTitle,
            MwFonts.Body,
            subtitleRect,
            MwColors.TextSecondary,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        graphics.Clear(Parent?.BackColor ?? MwColors.Page);

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using var path = GraphicsHelper.CreateRoundedRectangle(rect, Radius);
        using var backgroundBrush = new SolidBrush(CardBackColor);
        using var borderPen = new Pen(BorderColor, 1);

        graphics.FillPath(backgroundBrush, path);
        graphics.DrawPath(borderPen, path);

        DrawTitle(graphics);
        DrawSubtitle(graphics);
    }

 


}