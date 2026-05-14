using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwSegmentedButton : Button
{
    private bool _isHovered;
    private bool _isPressed;
    private bool _selected;
    private int _radius = MwRadius.Md;
    private string _iconText = string.Empty;
    private MwButtonSize _buttonSize = MwButtonSize.Medium;

    [Category("ModernWinFormsUI")]
    [Description("Indicates whether the segmented button is selected.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Optional icon text displayed before the label. Example: 🔎, 👤, #")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string IconText
    {
        get => _iconText;
        set
        {
            _iconText = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Size of the segmented button.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwButtonSize.Medium)]
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
    [Description("Corner radius of the segmented button.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwRadius.Md)]
    public int Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            Invalidate();
        }
    }

    public MwSegmentedButton()
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
        UseVisualStyleBackColor = false;

        Text = "Segment";
        Font = MwFonts.BodyBold;
        Cursor = Cursors.Hand;
        TabStop = true;

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
        using var borderPen = new Pen(GetBorderColor(), Selected ? 2 : 1);

        graphics.FillPath(backgroundBrush, path);
        graphics.DrawPath(borderPen, path);

        DrawContent(graphics, rect);

        if (Focused && Enabled)
            DrawFocusRing(graphics);
    }

    private void DrawContent(Graphics graphics, Rectangle rect)
    {
        var textColor = GetTextColor();

        if (string.IsNullOrWhiteSpace(IconText))
        {
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

            return;
        }

        int gap = 8;

        Size iconSize = TextRenderer.MeasureText(IconText, Font);
        Size textSize = TextRenderer.MeasureText(Text, Font);

        int totalWidth = iconSize.Width + gap + textSize.Width;
        int startX = rect.Left + Math.Max(0, (rect.Width - totalWidth) / 2);

        var iconRect = new Rectangle(
            startX,
            rect.Top,
            iconSize.Width,
            rect.Height
        );

        var textRect = new Rectangle(
            startX + iconSize.Width + gap,
            rect.Top,
            Math.Max(0, rect.Right - startX - iconSize.Width - gap),
            rect.Height
        );

        TextRenderer.DrawText(
            graphics,
            IconText,
            Font,
            iconRect,
            textColor,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter
        );

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
    }

    private void ApplySize()
    {
        Height = ButtonSize switch
        {
            MwButtonSize.Small => 34,
            MwButtonSize.Large => 48,
            _ => 40
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

        if (Selected)
        {
            if (_isPressed)
                return Color.FromArgb(219, 234, 254);

            if (_isHovered)
                return Color.FromArgb(219, 234, 254);

            return MwColors.PrimarySoft;
        }

        if (_isPressed)
            return Color.FromArgb(229, 231, 235);

        if (_isHovered)
            return MwColors.SurfaceSoft;

        return MwColors.Surface;
    }

    private Color GetBorderColor()
    {
        if (!Enabled)
            return MwColors.Border;

        if (Selected)
            return Color.FromArgb(147, 197, 253);

        return MwColors.Border;
    }

    private Color GetTextColor()
    {
        if (!Enabled)
            return MwColors.DisabledText;

        if (Selected)
            return MwColors.Primary;

        return MwColors.TextPrimary;
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

    private Color GetEffectiveParentBackColor()
    {
        Control? currentParent = Parent;

        while (currentParent is not null)
        {
            if (currentParent is MwCard card)
                return card.CardBackColor;

            if (currentParent.BackColor != Color.Transparent &&
                currentParent.BackColor != Color.Empty)
                return currentParent.BackColor;

            currentParent = currentParent.Parent;
        }

        return MwColors.Page;
    }
}