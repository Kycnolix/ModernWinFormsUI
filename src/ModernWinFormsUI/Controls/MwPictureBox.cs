using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwPictureBox : Control
{
    private Image? _image;
    private MwPictureBoxFit _fit = MwPictureBoxFit.Contain;
    private MwPictureBoxShape _shape = MwPictureBoxShape.RoundedRectangle;
    private MwPictureBoxEmptyState _emptyState = MwPictureBoxEmptyState.Blank;

    private string _placeholderText = string.Empty;
    private string _initials = string.Empty;

    private int _radius = MwRadius.Lg;
    private bool _showBorder = true;

    private Color _borderColor = MwColors.Border;
    private Color _pictureBackColor = MwColors.Surface;

    [Category("ModernWinFormsUI")]
    [Description("Image displayed inside the picture box.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? Image
    {
        get => _image;
        set
        {
            _image = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Determines how the image fits inside the picture box.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwPictureBoxFit.Contain)]
    public MwPictureBoxFit Fit
    {
        get => _fit;
        set
        {
            _fit = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Shape of the picture box.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwPictureBoxShape.RoundedRectangle)]
    public MwPictureBoxShape Shape
    {
        get => _shape;
        set
        {
            _shape = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("What should be displayed when there is no image.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwPictureBoxEmptyState.Blank)]
    public MwPictureBoxEmptyState EmptyState
    {
        get => _emptyState;
        set
        {
            _emptyState = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Placeholder text displayed when EmptyState is PlaceholderText.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string PlaceholderText
    {
        get => _placeholderText;
        set
        {
            _placeholderText = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Initials displayed when EmptyState is Initials.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string Initials
    {
        get => _initials;
        set
        {
            _initials = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Corner radius used when Shape is RoundedRectangle.")]
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
    [Description("Indicates whether the border is visible.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool ShowBorder
    {
        get => _showBorder;
        set
        {
            _showBorder = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Border color of the picture box.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
    [Description("Background color of the picture box area.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color PictureBackColor
    {
        get => _pictureBackColor;
        set
        {
            _pictureBackColor = value;
            Invalidate();
        }
    }

    public MwPictureBox()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor,
            true
        );

        Size = new Size(160, 120);
        MinimumSize = new Size(48, 48);
        BackColor = Color.Transparent;
        Font = MwFonts.BodyBold;
        TabStop = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        graphics.Clear(GetEffectiveParentBackColor());

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using var path = CreateShapePath(rect);
        using var backgroundBrush = new SolidBrush(GetBackgroundColor());

        graphics.FillPath(backgroundBrush, path);

        if (Image is not null)
        {
            DrawImage(graphics, path, rect);
        }
        else
        {
            DrawEmptyState(graphics, rect);
        }

        if (ShowBorder)
        {
            using var borderPen = new Pen(GetBorderColor(), 1);
            graphics.DrawPath(borderPen, path);
        }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    private GraphicsPath CreateShapePath(Rectangle rect)
    {
        if (Shape == MwPictureBoxShape.Circle)
        {
            int size = Math.Min(rect.Width, rect.Height);
            int x = rect.X + (rect.Width - size) / 2;
            int y = rect.Y + (rect.Height - size) / 2;

            var path = new GraphicsPath();
            path.AddEllipse(x, y, size, size);
            path.CloseFigure();
            return path;
        }

        return GraphicsHelper.CreateRoundedRectangle(rect, Radius);
    }

    private void DrawImage(Graphics graphics, GraphicsPath clipPath, Rectangle bounds)
    {
        if (Image is null)
            return;

        var destination = GetImageDestinationRectangle(Image, bounds);

        using var previousClip = graphics.Clip.Clone();

        graphics.SetClip(clipPath);
        graphics.DrawImage(Image, destination);
        graphics.SetClip(previousClip, CombineMode.Replace);
    }

    private Rectangle GetImageDestinationRectangle(Image image, Rectangle bounds)
    {
        if (Fit == MwPictureBoxFit.Stretch)
            return bounds;

        float imageRatio = (float)image.Width / image.Height;
        float boxRatio = (float)bounds.Width / bounds.Height;

        int width;
        int height;

        if (Fit == MwPictureBoxFit.Contain)
        {
            if (imageRatio > boxRatio)
            {
                width = bounds.Width;
                height = (int)(bounds.Width / imageRatio);
            }
            else
            {
                height = bounds.Height;
                width = (int)(bounds.Height * imageRatio);
            }
        }
        else
        {
            if (imageRatio > boxRatio)
            {
                height = bounds.Height;
                width = (int)(bounds.Height * imageRatio);
            }
            else
            {
                width = bounds.Width;
                height = (int)(bounds.Width / imageRatio);
            }
        }

        int x = bounds.X + (bounds.Width - width) / 2;
        int y = bounds.Y + (bounds.Height - height) / 2;

        return new Rectangle(x, y, width, height);
    }

    private void DrawEmptyState(Graphics graphics, Rectangle rect)
    {
        if (EmptyState == MwPictureBoxEmptyState.Blank)
            return;

        if (EmptyState == MwPictureBoxEmptyState.Initials)
        {
            DrawInitials(graphics, rect);
            return;
        }

        DrawPlaceholderText(graphics, rect);
    }

    private void DrawInitials(Graphics graphics, Rectangle rect)
    {
        string text = string.IsNullOrWhiteSpace(Initials)
            ? "?"
            : Initials.Trim().ToUpperInvariant();

        using var font = new Font("Segoe UI", Math.Max(16, Math.Min(Width, Height) / 5f), FontStyle.Bold);

        TextRenderer.DrawText(
            graphics,
            text,
            font,
            rect,
            Enabled ? MwColors.Primary : MwColors.DisabledText,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }

    private void DrawPlaceholderText(Graphics graphics, Rectangle rect)
    {
        string text = string.IsNullOrWhiteSpace(PlaceholderText)
            ? "No image"
            : PlaceholderText;

        TextRenderer.DrawText(
            graphics,
            text,
            MwFonts.Body,
            rect,
            Enabled ? MwColors.TextMuted : MwColors.DisabledText,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.WordBreak |
            TextFormatFlags.EndEllipsis
        );
    }

    private Color GetBackgroundColor()
    {
        if (!Enabled)
            return MwColors.DisabledBackground;


        if (PictureBackColor == Color.Transparent || PictureBackColor == Color.Empty)
            return GetEffectiveParentBackColor();

        return PictureBackColor;
    }
    
    private Color GetBorderColor()
    {
        if (!Enabled)
            return MwColors.Border;

        return BorderColor;
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