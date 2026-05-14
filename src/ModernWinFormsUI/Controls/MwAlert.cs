using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwAlert : Control
{
    private MwAlertVariant _variant = MwAlertVariant.Info;
    private string _title = "Bilgilendirme";
    private string _message = "Bu bir bilgilendirme mesajıdır.";
    private int _radius = MwRadius.Lg;
    private bool _autoHeight = true;

    [Category("ModernWinFormsUI")]
    [Description("Visual variant of the alert.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwAlertVariant.Info)]
    public MwAlertVariant Variant
    {
        get => _variant;
        set
        {
            _variant = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Alert title text.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("Bilgilendirme")]
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            ApplyRecommendedHeight();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Alert message text.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("Bu bir bilgilendirme mesajıdır.")]
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            ApplyRecommendedHeight();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Corner radius of the alert.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwRadius.Lg)]
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
    [Description("Automatically adjusts alert height based on content.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool AutoHeight
    {
        get => _autoHeight;
        set
        {
            _autoHeight = value;
            ApplyRecommendedHeight();
            Invalidate();
        }
    }

    // Designer'ın default Text alanını kullanmasını istemiyoruz.
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => Message;
        set => Message = value;
    }

    public MwAlert()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true
        );

        Size = new Size(420, 82);
        MinimumSize = new Size(220, 56);
        Font = MwFonts.Body;
        TabStop = false;

        ApplyRecommendedHeight();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (AutoHeight)
            ApplyRecommendedHeight();

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        graphics.Clear(GetEffectiveParentBackColor());

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using var path = GraphicsHelper.CreateRoundedRectangle(rect, Radius);
        using var backgroundBrush = new SolidBrush(GetBackgroundColor());
        using var borderPen = new Pen(GetBorderColor(), 1);

        graphics.FillPath(backgroundBrush, path);
        graphics.DrawPath(borderPen, path);

        DrawAccent(graphics);
        DrawIcon(graphics);
        DrawTextContent(graphics);
    }

    private void DrawAccent(Graphics graphics)
    {
        var accentRect = new Rectangle(0, 0, 5, Height - 1);

        using var path = GraphicsHelper.CreateRoundedRectangle(
            accentRect,
            Radius
        );

        using var brush = new SolidBrush(GetAccentColor());

        graphics.FillPath(brush, path);
    }

    private void DrawIcon(Graphics graphics)
    {
        var iconRect = new Rectangle(16, 20, 28, 28);

        using var brush = new SolidBrush(GetAccentColor());
        using var textBrush = new SolidBrush(Color.White);

        graphics.FillEllipse(brush, iconRect);

        string iconText = Variant switch
        {
            MwAlertVariant.Success => "✓",
            MwAlertVariant.Warning => "!",
            MwAlertVariant.Danger => "!",
            _ => "i"
        };

        TextRenderer.DrawText(
            graphics,
            iconText,
            MwFonts.BodyBold,
            iconRect,
            Color.White,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter
        );
    }

    private void DrawTextContent(Graphics graphics)
    {
        int textLeft = 56;
        int textWidth = Math.Max(0, Width - textLeft - 16);

        var titleRect = new Rectangle(
            textLeft,
            14,
            textWidth,
            24
        );

        TextRenderer.DrawText(
            graphics,
            Title,
            MwFonts.BodyBold,
            titleRect,
            MwColors.TextPrimary,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );

        var messageRect = new Rectangle(
            textLeft,
            38,
            textWidth,
            Height - 44
        );

        TextRenderer.DrawText(
            graphics,
            Message,
            MwFonts.Body,
            messageRect,
            MwColors.TextSecondary,
            TextFormatFlags.Left |
            TextFormatFlags.Top |
            TextFormatFlags.WordBreak |
            TextFormatFlags.EndEllipsis
        );
    }

    private void ApplyRecommendedHeight()
    {
        if (!AutoHeight)
            return;

        int textWidth = Math.Max(120, Width - 72);

        Size messageSize = TextRenderer.MeasureText(
            Message,
            MwFonts.Body,
            new Size(textWidth, 0),
            TextFormatFlags.WordBreak
        );

        int recommendedHeight = Math.Max(
            64,
            44 + messageSize.Height + 12
        );

        if (Height != recommendedHeight)
            Height = recommendedHeight;
    }

    private Color GetBackgroundColor()
    {
        return Variant switch
        {
            MwAlertVariant.Success => MwColors.SuccessSoft,
            MwAlertVariant.Warning => MwColors.WarningSoft,
            MwAlertVariant.Danger => MwColors.DangerSoft,
            _ => MwColors.InfoSoft
        };
    }

    private Color GetBorderColor()
    {
        return Variant switch
        {
            MwAlertVariant.Success => Color.FromArgb(187, 247, 208),
            MwAlertVariant.Warning => Color.FromArgb(253, 230, 138),
            MwAlertVariant.Danger => Color.FromArgb(254, 202, 202),
            _ => Color.FromArgb(186, 230, 253)
        };
    }

    private Color GetAccentColor()
    {
        return Variant switch
        {
            MwAlertVariant.Success => MwColors.Success,
            MwAlertVariant.Warning => MwColors.Warning,
            MwAlertVariant.Danger => MwColors.Danger,
            _ => MwColors.Info
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