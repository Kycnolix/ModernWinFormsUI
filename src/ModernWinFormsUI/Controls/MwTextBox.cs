using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

[ToolboxItem(true)]
public class MwTextBox : UserControl
{
    private const int InputHeight = 38;
    private const int LabelAreaHeight = 24;
    private const int FooterAreaHeight = 24;
    private const int BottomSafeArea = 4;
    private const int InnerTextBoxHeight = 24;

    private bool _readOnly;

    private readonly TextBox _textBox = new();

    private string _labelText = string.Empty;
    private string _helperText = string.Empty;
    private string _errorText = string.Empty;
    private int _radius = MwRadius.Md;
    private bool _isFocused;
    private bool _autoHeight = true;

    [Category("ModernWinFormsUI")]
    [Description("Label text displayed above the input.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string LabelText
    {
        get => _labelText;
        set
        {
            _labelText = value;
            ApplyRecommendedHeight();
            LayoutInnerTextBox();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Helper text displayed below the input.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string HelperText
    {
        get => _helperText;
        set
        {
            _helperText = value;
            ApplyRecommendedHeight();
            Invalidate();
        }
    }



    [Category("ModernWinFormsUI")]
    [Description("Error text displayed below the input. When set, the border becomes red.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string ErrorText
    {
        get => _errorText;
        set
        {
            _errorText = value;
            ApplyRecommendedHeight();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Placeholder text displayed when the input is empty.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string PlaceholderText
    {
        get => _textBox.PlaceholderText;
        set => _textBox.PlaceholderText = value;
    }

    [Category("ModernWinFormsUI")]
    [Description("Corner radius of the input border.")]
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

    [Category("ModernWinFormsUI")]
    [Description("Automatically adjusts the control height based on label and helper/error text.")]
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

    // Designer'ın Text alanına otomatik "mwTextBox1" yazmasını engelliyoruz.
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => _textBox.Text;
        set => _textBox.Text = value;
    }

    [Category("ModernWinFormsUI")]
    [DisplayName("Text")]
    [Description("Current input value.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string InputText
    {
        get => _textBox.Text;
        set => _textBox.Text = value;
    }

    [Category("ModernWinFormsUI")]
    [Description("Indicates whether the input is read-only.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => _readOnly;
        set
        {
            _readOnly = value;
            _textBox.ReadOnly = value;
            ApplyTextBoxVisualState();
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Indicates whether the text should be displayed as password characters.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get => _textBox.UseSystemPasswordChar;
        set => _textBox.UseSystemPasswordChar = value;
    }

    [Category("ModernWinFormsUI")]
    [Description("Custom password character. Ignored when UseSystemPasswordChar is true.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue('\0')]
    public char PasswordChar
    {
        get => _textBox.PasswordChar;
        set => _textBox.PasswordChar = value;
    }

    public MwTextBox()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true
        );

        Size = new Size(280, InputHeight);
        MinimumSize = new Size(120, InputHeight);
        BackColor = Color.Transparent;
        Font = MwFonts.Body;

        _textBox.BorderStyle = BorderStyle.None;
        _textBox.Font = MwFonts.Body;
        _textBox.BackColor = MwColors.Surface;
        _textBox.ForeColor = MwColors.TextPrimary;
        _textBox.AutoSize = false;
        ApplyTextBoxVisualState();
        _textBox.TabStop = true;


        _textBox.GotFocus += (_, _) =>
        {
            _isFocused = true;
            Invalidate();
        };

        _textBox.LostFocus += (_, _) =>
        {
            _isFocused = false;
            Invalidate();
        };

        _textBox.TextChanged += (_, _) =>
        {
            OnTextChanged(EventArgs.Empty);
        };

        Controls.Add(_textBox);

        ApplyRecommendedHeight();
        LayoutInnerTextBox();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (AutoHeight)
            ApplyRecommendedHeight();

        LayoutInnerTextBox();
        Invalidate();
    }

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        _textBox.Focus();
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);

        _textBox.Enabled = Enabled;
        ApplyTextBoxVisualState();

        Invalidate();
    }
    private void ApplyTextBoxVisualState()
    {
        _textBox.BackColor = GetInputBackgroundColor();
        _textBox.ForeColor = Enabled ? MwColors.TextPrimary : MwColors.DisabledText;
        _textBox.Cursor = Enabled && !ReadOnly ? Cursors.IBeam : Cursors.Default;
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        graphics.Clear(GetEffectiveParentBackColor());

        DrawLabel(graphics);
        DrawInputBorder(graphics);
        DrawFooterText(graphics);
    }

    private void ApplyRecommendedHeight()
    {
        if (!AutoHeight)
            return;

        int recommendedHeight = GetRecommendedHeight();

        if (Height != recommendedHeight)
            Height = recommendedHeight;
    }

    private int GetRecommendedHeight()
    {
        int height = InputHeight + BottomSafeArea;

        if (!string.IsNullOrWhiteSpace(LabelText))
            height += LabelAreaHeight;

        if (!string.IsNullOrWhiteSpace(ErrorText) || !string.IsNullOrWhiteSpace(HelperText))
            height += FooterAreaHeight;

        return height;
    }

    private void LayoutInnerTextBox()
    {
        var inputRect = GetInputRectangle();

        int textBoxTop = inputRect.Top + ((inputRect.Height - InnerTextBoxHeight) / 2);

        _textBox.Location = new Point(
            inputRect.Left + MwSpacing.Md,
            textBoxTop
        );

        _textBox.Width = Math.Max(0, inputRect.Width - (MwSpacing.Md * 2));
        _textBox.Height = InnerTextBoxHeight;
    }

    private Rectangle GetInputRectangle()
    {
        int top = string.IsNullOrWhiteSpace(LabelText) ? 0 : LabelAreaHeight;

        return new Rectangle(
               0,
               top,
               Width - 1,
               InputHeight - 1
           );
    }

    private void DrawLabel(Graphics graphics)
    {
        if (string.IsNullOrWhiteSpace(LabelText))
            return;

        var labelRect = new Rectangle(0, 0, Width, 20);

        TextRenderer.DrawText(
            graphics,
            LabelText,
            MwFonts.BodyBold,
            labelRect,
            MwColors.TextPrimary,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }
    private Color GetInputBackgroundColor()
    {
        if (!Enabled)
            return MwColors.DisabledBackground;

        if (ReadOnly)
            return MwColors.SurfaceSoft;

        return MwColors.Surface;
    }
    private void DrawInputBorder(Graphics graphics)
    {
        var inputRect = GetInputRectangle();

        var borderColor = GetBorderColor();
        var backgroundColor = GetInputBackgroundColor();

        using var path = GraphicsHelper.CreateRoundedRectangle(inputRect, Radius);
        using var backgroundBrush = new SolidBrush(backgroundColor);
        using var borderPen = new Pen(borderColor, _isFocused ? 2 : 1);

        graphics.FillPath(backgroundBrush, path);
        graphics.DrawPath(borderPen, path);
    }

    private void DrawFooterText(Graphics graphics)
    {
        string footerText = !string.IsNullOrWhiteSpace(ErrorText)
            ? ErrorText
            : HelperText;

        if (string.IsNullOrWhiteSpace(footerText))
            return;

        var inputRect = GetInputRectangle();

        var footerRect = new Rectangle(
            0,
            inputRect.Bottom + 4,
            Width,
            20
        );

        var footerColor = !string.IsNullOrWhiteSpace(ErrorText)
            ? MwColors.Danger
            : MwColors.TextSecondary;

        TextRenderer.DrawText(
            graphics,
            footerText,
            MwFonts.Small,
            footerRect,
            footerColor,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }

    private Color GetBorderColor()
    {
        if (!Enabled)
            return MwColors.Border;

        if (!string.IsNullOrWhiteSpace(ErrorText))
            return MwColors.Danger;

        if (_isFocused)
            return MwColors.Primary;

        return MwColors.Border;
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