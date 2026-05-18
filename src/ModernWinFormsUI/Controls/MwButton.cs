using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

public class MwButton : Button
{
    private bool _isHovered;
    private bool _isPressed;
    private Image? _iconImage;
    private int _iconSize = 18;
    private int _iconGap = 8;

    private MwButtonVariant _variant = MwButtonVariant.Primary;
    private MwButtonSize _buttonSize = MwButtonSize.Medium;
    private int _radius = MwRadius.Md;

    [Category("ModernWinFormsUI")]
    public MwButtonVariant Variant
    {
        get => _variant;
        set
        {
            _variant = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    public MwButtonSize ButtonSize
    {
        get => _buttonSize;
        set
        {
            _buttonSize = value;
            ApplySize();
            Invalidate();
        }
    }

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
    [Description("Image displayed before the button text. Recommended: transparent PNG or ICO.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public Image? IconImage
    {
        get => _iconImage;
        set
        {
            _iconImage = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Size of the button icon image.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(18)]
    public int IconSize
    {
        get => _iconSize;
        set
        {
            _iconSize = ClampInt(value, 12, 40);
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Space between icon and text.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(8)]
    public int IconGap
    {
        get => _iconGap;
        set
        {
            _iconGap = ClampInt(value, 0, 24);
            Invalidate();
        }
    }

    private static int ClampInt(int value, int min, int max)
    {
        if (value < min)
            return min;

        if (value > max)
            return max;

        return value;
    }

    private static void DrawIconImage(Graphics graphics, Image image, Rectangle rect, bool enabled)
    {
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        if (enabled)
        {
            graphics.DrawImage(image, rect);
            return;
        }

        using var imageAttributes = new System.Drawing.Imaging.ImageAttributes();

        var colorMatrix = new System.Drawing.Imaging.ColorMatrix
        {
            Matrix33 = 0.45f
        };

        imageAttributes.SetColorMatrix(colorMatrix);

        graphics.DrawImage(
            image,
            rect,
            0,
            0,
            image.Width,
            image.Height,
            GraphicsUnit.Pixel,
            imageAttributes
        );
    }

    private void DrawContent(Graphics graphics, Rectangle rect, Color textColor)
    {
        if (IconImage is not null)
        {
            int iconSize = Math.Min(IconSize, rect.Height - 12);

            Size textSize = TextRenderer.MeasureText(Text, Font);

            int totalWidth = iconSize + IconGap + textSize.Width;
            int startX = rect.Left + Math.Max(0, (rect.Width - totalWidth) / 2);

            var iconRect = new Rectangle(
                startX,
                rect.Top + (rect.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            var textRect = new Rectangle(
                iconRect.Right + IconGap,
                rect.Top,
                Math.Max(0, rect.Right - iconRect.Right - IconGap),
                rect.Height
            );

            DrawIconImage(graphics, IconImage, iconRect, Enabled);

            TextRenderer.DrawText(
                graphics,
                Text,
                Font,
                textRect,
                textColor,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis
            );

            return;
        }

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

    public MwButton()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true
        );

        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;

        Font = MwFonts.BodyBold;
        Cursor = Cursors.Hand;
        TabStop = true;
        UseVisualStyleBackColor = false;

        ApplySize();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        _isHovered = true;
        Invalidate();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _isHovered = false;
        _isPressed = false;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        if (mevent.Button == MouseButtons.Left && Enabled)
        {
            _isPressed = true;
            Invalidate();
        }

        base.OnMouseDown(mevent);
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        _isPressed = false;
        Invalidate();

        base.OnMouseUp(mevent);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        Cursor = Enabled ? Cursors.Hand : Cursors.Default;
        Invalidate();

        base.OnEnabledChanged(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        Invalidate();
        base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
        Invalidate();
        base.OnLostFocus(e);
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        var graphics = pevent.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        graphics.Clear(GetEffectiveParentBackColor());

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using var path = GraphicsHelper.CreateRoundedRectangle(rect, Radius);
        using var backgroundBrush = new SolidBrush(GetBackgroundColor());
        using var borderPen = new Pen(GetBorderColor(), 1);

        graphics.FillPath(backgroundBrush, path);
        graphics.DrawPath(borderPen, path);

        var textColor = GetTextColor();

        DrawContent(graphics, rect, textColor);

        if (Focused && Enabled)
            DrawFocusRing(graphics);
    }

    private void ApplySize()
    {
        Height = ButtonSize switch
        {
            MwButtonSize.Small => 32,
            MwButtonSize.Large => 44,
            _ => 38
        };

        Padding = ButtonSize switch
        {
            MwButtonSize.Small => new Padding(MwSpacing.Md, 0, MwSpacing.Md, 0),
            MwButtonSize.Large => new Padding(MwSpacing.Xl, 0, MwSpacing.Xl, 0),
            _ => new Padding(MwSpacing.Lg, 0, MwSpacing.Lg, 0)
        };
    }

    private Color GetBackgroundColor()
    {
        if (!Enabled)
            return MwColors.DisabledBackground;

        return Variant switch
        {
            MwButtonVariant.Primary => GetPrimaryBackground(),
            MwButtonVariant.Secondary => GetSecondaryBackground(),
            MwButtonVariant.Danger => GetDangerBackground(),
            MwButtonVariant.Ghost => GetGhostBackground(),
            _ => MwColors.Primary
        };
    }

    private Color GetPrimaryBackground()
    {
        if (_isPressed) return MwColors.PrimaryPressed;
        if (_isHovered) return MwColors.PrimaryHover;

        return MwColors.Primary;
    }

    private Color GetSecondaryBackground()
    {
        if (_isPressed) return Color.FromArgb(229, 231, 235);
        if (_isHovered) return MwColors.SurfaceSoft;

        return MwColors.Surface;
    }

    private Color GetDangerBackground()
    {
        if (_isPressed) return MwColors.DangerPressed;
        if (_isHovered) return MwColors.DangerHover;

        return MwColors.Danger;
    }

    private Color GetGhostBackground()
    {
        if (_isPressed) return Color.FromArgb(219, 234, 254);
        if (_isHovered) return MwColors.PrimarySoft;

        return Parent?.BackColor ?? MwColors.Page;
    }

    private Color GetBorderColor()
    {
        if (!Enabled)
            return MwColors.Border;

        return Variant switch
        {
            MwButtonVariant.Secondary => MwColors.Border,
            MwButtonVariant.Ghost => Color.Transparent,
            _ => GetBackgroundColor()
        };
    }

    private Color GetTextColor()
    {
        if (!Enabled)
            return MwColors.DisabledText;

        return Variant switch
        {
            MwButtonVariant.Primary => MwColors.TextInverse,
            MwButtonVariant.Danger => MwColors.TextInverse,
            MwButtonVariant.Secondary => MwColors.TextPrimary,
            MwButtonVariant.Ghost => MwColors.Primary,
            _ => MwColors.TextPrimary
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
    private void DrawFocusRing(Graphics graphics)
    {
        var focusRect = new Rectangle(3, 3, Width - 7, Height - 7);

        using var focusPath = GraphicsHelper.CreateRoundedRectangle(
            focusRect,
            Math.Max(2, Radius - 3)
        );

        using var focusPen = new Pen(Color.FromArgb(147, 197, 253), 2);

        graphics.DrawPath(focusPen, focusPath);
    }
}