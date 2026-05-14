using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwBadge : Control
{
    private MwBadgeVariant _variant = MwBadgeVariant.Neutral;
    private MwBadgeSize _badgeSize = MwBadgeSize.Medium;
    private bool _autoWidth = true;

    [Category("ModernWinFormsUI")]
    [Description("Visual variant of the badge.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwBadgeVariant.Neutral)]
    public MwBadgeVariant Variant
    {
        get => _variant;
        set
        {
            _variant = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Size of the badge.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwBadgeSize.Medium)]
    public MwBadgeSize BadgeSize
    {
        get => _badgeSize;
        set
        {
            _badgeSize = value;
            ApplyRecommendedSize();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Automatically adjusts badge width based on text.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool AutoWidth
    {
        get => _autoWidth;
        set
        {
            _autoWidth = value;
            ApplyRecommendedSize();
            Invalidate();
        }
    }

    public MwBadge()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true
        );

        Text = "Badge";
        Font = MwFonts.SmallBold;
        TabStop = false;

        ApplyRecommendedSize();
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);

        ApplyRecommendedSize();
        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (AutoWidth)
            ApplyRecommendedSize();

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        graphics.Clear(GetEffectiveParentBackColor());

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        var backgroundColor = GetBackgroundColor();
        var borderColor = GetBorderColor();
        var textColor = GetTextColor();

        using var path = GraphicsHelper.CreateRoundedRectangle(rect, Height / 2);
        using var backgroundBrush = new SolidBrush(backgroundColor);
        using var borderPen = new Pen(borderColor, 1);

        graphics.FillPath(backgroundBrush, path);
        graphics.DrawPath(borderPen, path);

        TextRenderer.DrawText(
            graphics,
            Text,
            Font,
            rect,
            textColor,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }

    private void ApplyRecommendedSize()
    {
        Height = BadgeSize switch
        {
            MwBadgeSize.Small => 22,
            _ => 26
        };

        Font = BadgeSize switch
        {
            MwBadgeSize.Small => MwFonts.SmallBold,
            _ => MwFonts.BodyBold
        };

        if (!AutoWidth)
            return;

        int horizontalPadding = BadgeSize switch
        {
            MwBadgeSize.Small => 18,
            _ => 22
        };

        int textWidth = TextRenderer.MeasureText(Text, Font).Width;

        Width = Math.Max(44, textWidth + horizontalPadding);
    }

    private Color GetBackgroundColor()
    {
        return Variant switch
        {
            MwBadgeVariant.Success => MwColors.SuccessSoft,
            MwBadgeVariant.Warning => MwColors.WarningSoft,
            MwBadgeVariant.Danger => MwColors.DangerSoft,
            MwBadgeVariant.Info => MwColors.InfoSoft,
            _ => MwColors.SurfaceSoft
        };
    }

    private Color GetBorderColor()
    {
        return Variant switch
        {
            MwBadgeVariant.Success => Color.FromArgb(187, 247, 208),
            MwBadgeVariant.Warning => Color.FromArgb(253, 230, 138),
            MwBadgeVariant.Danger => Color.FromArgb(254, 202, 202),
            MwBadgeVariant.Info => Color.FromArgb(186, 230, 253),
            _ => MwColors.Border
        };
    }

    private Color GetTextColor()
    {
        return Variant switch
        {
            MwBadgeVariant.Success => MwColors.Success,
            MwBadgeVariant.Warning => MwColors.Warning,
            MwBadgeVariant.Danger => MwColors.Danger,
            MwBadgeVariant.Info => MwColors.Info,
            _ => MwColors.TextSecondary
        };
    }

    private Color GetEffectiveParentBackColor()
    {
        Control? currentParent = Parent;

        while (currentParent is not null)
        {
            if (currentParent is MwCard card)
                return card.CardBackColor;

            if (currentParent.BackColor != Color.Transparent &&
                currentParent.BackColor != Color.Empty)
            {
                return currentParent.BackColor;
            }

            currentParent = currentParent.Parent;
        }

        return MwColors.Page;
    }
}