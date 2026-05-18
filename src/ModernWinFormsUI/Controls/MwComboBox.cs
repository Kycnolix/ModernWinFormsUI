using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ModernWinFormsUI.Tokens;
using ModernWinFormsUI.Utilities;

namespace ModernWinFormsUI.Controls;

public enum MwComboBoxIcon
{
    None,
    Printer
}

[ToolboxItem(true)]
public class MwComboBox : Control
{
    private const int InputHeight = 44;
    private const int LabelAreaHeight = 24;
    private const int FooterAreaHeight = 24;
    private const int BottomSafeArea = 4;
    private const int HorizontalPadding = 14;
    private const int ChevronAreaWidth = 34;

    private readonly List<object> _items = new();
    private Image? _iconImage;
    private int _iconSize = 20;
    private int _iconGap = 10;

    private string _labelText = string.Empty;
    private string _helperText = string.Empty;
    private string _errorText = string.Empty;
    private string _placeholderText = "Select an option";

    private int _selectedIndex = -1;
    private int _radius = 10;
    private int _dropDownMaxVisibleItems = 8;

    private bool _isFocused;
    private bool _isHovered;
    private bool _isPressed;
    private bool _autoHeight = true;

    private MwComboBoxIcon _icon = MwComboBoxIcon.None;

    private ToolStripDropDown? _dropDown;
    private ListBox? _listBox;

    private bool IsDropDownOpen =>
    _dropDown is not null && !_dropDown.IsDisposed && _dropDown.Visible;

    // Geriye uyumluluk için bırakıyoruz. Görseli artık native ComboBox çizmediği için etkisi yok.
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ComboBoxStyle DropDownStyle { get; set; } = ComboBoxStyle.DropDownList;

    public event EventHandler? SelectedIndexChanged;

    [Category("ModernWinFormsUI")]
    [Description("Label text displayed above the combo box.")]
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
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Helper text displayed below the combo box.")]
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
    [Description("Error text displayed below the combo box.")]
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
    [Description("Placeholder text displayed when no item is selected.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("Select an option")]
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
    [Description("Icon displayed before the selected text.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(MwComboBoxIcon.None)]
    public MwComboBoxIcon Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Selected item index.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (value < -1 || value >= _items.Count)
                return;

            if (_selectedIndex == value)
                return;

            _selectedIndex = value;
            Invalidate();
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get
        {
            if (_selectedIndex < 0 || _selectedIndex >= _items.Count)
                return null;

            return _items[_selectedIndex];
        }
        set
        {
            int index = value is null ? -1 : _items.IndexOf(value);
            SelectedIndex = index;
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<object> Items => _items.AsReadOnly();

    [Category("ModernWinFormsUI")]
    [Description("Corner radius of the combo box.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(10)]
    public int Radius
    {
        get => _radius;
        set
        {
            _radius = Math.Max(0, value);
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
    [Category("ModernWinFormsUI")]
    [Description("Image displayed before the selected text. Recommended: transparent PNG or ICO.")]
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
    [Description("Size of the leading icon image.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(20)]
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
    [DefaultValue(10)]
    public int IconGap
    {
        get => _iconGap;
        set
        {
            _iconGap = ClampInt(value, 0, 24);
            Invalidate();
        }
    }

    [Category("ModernWinFormsUI")]
    [Description("Maximum visible item count in dropdown.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(8)]
    public int DropDownMaxVisibleItems
    {
        get => _dropDownMaxVisibleItems;
        set => _dropDownMaxVisibleItems = Math.Max(1, value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => GetDisplayText();
        set { }
    }

    public MwComboBox()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor |
            ControlStyles.Selectable,
            true
        );

        Size = new Size(360, InputHeight + BottomSafeArea);
        MinimumSize = new Size(160, InputHeight);
        BackColor= Color.Transparent;

        Font = new Font("Segoe UI", 9.75f, FontStyle.Regular);

        Cursor = Cursors.Hand;
        TabStop = true;

        ApplyRecommendedHeight();
    }

    public void AddItem(object item)
    {
        _items.Add(item);
        Invalidate();
    }

    public void AddItems(object[] items)
    {
        _items.AddRange(items);
        Invalidate();
    }

    public void ClearItems()
    {
        _items.Clear();
        SelectedIndex = -1;
        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (AutoHeight)
            ApplyRecommendedHeight();

        Invalidate();
    }

    protected override void OnEnter(EventArgs e)
    {
        _isFocused = true;
        Invalidate();
        base.OnEnter(e);
    }

    protected override void OnLeave(EventArgs e)
    {
        _isFocused = false;
        Invalidate();
        base.OnLeave(e);
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

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (!Enabled)
            return;

        Focus();

        if (GetInputRectangle().Contains(e.Location))
        {
            _isPressed = true;
            Invalidate();
            ToggleDropDown();
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        _isPressed = false;
        Invalidate();
        base.OnMouseUp(e);
    }

    protected override bool IsInputKey(Keys keyData)
    {
        if (keyData == Keys.Down ||
            keyData == Keys.Up ||
            keyData == Keys.Enter ||
            keyData == Keys.Space)
        {
            return true;
        }

        return base.IsInputKey(keyData);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (!Enabled)
            return;

        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.Down)
        {
            ShowDropDown();
            e.Handled = true;
            return;
        }

        if (e.KeyCode == Keys.Up && _items.Count > 0)
        {
            SelectedIndex = Math.Max(0, SelectedIndex - 1);
            e.Handled = true;
        }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Cursor = Enabled ? Cursors.Hand : Cursors.Default;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        graphics.Clear(GetEffectiveParentBackColor());

        DrawLabel(graphics);
        DrawInput(graphics);
        DrawFooterText(graphics);
    }

    private void CloseDropDown()
    {
        if (_dropDown is null)
            return;

        if (!_dropDown.IsDisposed)
            _dropDown.Close();

        _dropDown = null;
        _listBox = null;

        Invalidate();
    }

    private void ToggleDropDown()
    {
        if (IsDropDownOpen)
        {
            CloseDropDown();
            return;
        }

        ShowDropDown();
    }

    private void ShowDropDown()
    {
        if (DesignMode)
            return;

        if (_items.Count == 0)
            return;

        if (IsDropDownOpen)
            return;

        CloseDropDown();

        var inputRect = GetInputRectangle();

        _listBox = new ListBox
        {
            BorderStyle = BorderStyle.None,
            DrawMode = DrawMode.OwnerDrawFixed,
            ItemHeight = 36,
            Font = Font,
            BackColor = MwColors.Surface,
            ForeColor = MwColors.TextPrimary,
            IntegralHeight = false
        };

        foreach (var item in _items)
            _listBox.Items.Add(item?.ToString() ?? string.Empty);

        if (SelectedIndex >= 0 && SelectedIndex < _listBox.Items.Count)
            _listBox.SelectedIndex = SelectedIndex;

        _listBox.DrawItem += DrawDropDownItem;
        _listBox.Click += (_, _) => SelectFromDropDown();
        _listBox.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectFromDropDown();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Escape)
            {
                _dropDown?.Close();
                Focus();
                e.Handled = true;
            }
        };

        int visibleCount = Math.Min(_items.Count, DropDownMaxVisibleItems);
        int dropdownHeight = Math.Max(36, visibleCount * _listBox.ItemHeight);

        _listBox.Size = new Size(inputRect.Width, dropdownHeight);

        var host = new ToolStripControlHost(_listBox)
        {
            Margin = Padding.Empty,
            Padding = Padding.Empty,
            AutoSize = false,
            Size = _listBox.Size
        };

        _dropDown = new ToolStripDropDown
        {
            Padding = Padding.Empty,
            Margin = Padding.Empty,
            AutoClose = true
        };

        _dropDown.Items.Add(host);
        var currentDropDown = _dropDown;

        _dropDown.Closed += (_, _) =>
        {
            if (ReferenceEquals(_dropDown, currentDropDown))
            {
                _dropDown = null;
                _listBox = null;
                Invalidate();
            }
        };

        _dropDown.Show(this, new Point(inputRect.Left, inputRect.Bottom + 6));
        _listBox.Focus();
    }

    private void SelectFromDropDown()
    {
        if (_listBox is null)
            return;

        if (_listBox.SelectedIndex >= 0)
            SelectedIndex = _listBox.SelectedIndex;

        CloseDropDown();
        Focus();
    }

    private void DrawDropDownItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= _items.Count)
            return;

        bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

        using var backgroundBrush = new SolidBrush(
            selected ? MwColors.PrimarySoft : MwColors.Surface
        );

        e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

        var textRect = new Rectangle(
            e.Bounds.Left + 14,
            e.Bounds.Top,
            e.Bounds.Width - 28,
            e.Bounds.Height
        );

        TextRenderer.DrawText(
            e.Graphics,
            _items[e.Index]?.ToString() ?? string.Empty,
            Font,
            textRect,
            selected ? MwColors.Primary : MwColors.TextPrimary,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
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

    private void DrawInput(Graphics graphics)
    {
        var inputRect = GetInputRectangle();

        using var path = GraphicsHelper.CreateRoundedRectangle(inputRect, Radius);
        using var backgroundBrush = new SolidBrush(GetInputBackgroundColor());
        using var borderPen = new Pen(GetBorderColor(), GetBorderWidth());

        graphics.FillPath(backgroundBrush, path);

        int contentLeft = inputRect.Left + HorizontalPadding;

        if (IconImage is not null)
        {
            int iconSize = Math.Min(IconSize, inputRect.Height - 12);

            var iconRect = new Rectangle(
                contentLeft,
                inputRect.Top + (inputRect.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            DrawIconImage(graphics, IconImage, iconRect, Enabled);

            contentLeft = iconRect.Right + IconGap;
        }
        else if (Icon != MwComboBoxIcon.None)
        {
            int iconSize = Math.Min(IconSize, inputRect.Height - 12);

            var iconRect = new Rectangle(
                contentLeft,
                inputRect.Top + (inputRect.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            DrawIcon(graphics, iconRect, Enabled ? MwColors.TextPrimary : MwColors.DisabledText);

            contentLeft = iconRect.Right + IconGap;
        }

        var chevronRect = new Rectangle(
            inputRect.Right - ChevronAreaWidth,
            inputRect.Top,
            ChevronAreaWidth,
            inputRect.Height
        );

        var textRect = new Rectangle(
            contentLeft,
            inputRect.Top,
            Math.Max(0, chevronRect.Left - contentLeft - 6),
            inputRect.Height
        );

        bool hasValue = SelectedIndex >= 0;
        string displayText = hasValue ? GetDisplayText() : PlaceholderText;

        TextRenderer.DrawText(
            graphics,
            displayText,
            Font,
            textRect,
            Enabled
                ? hasValue ? MwColors.TextPrimary : MwColors.TextMuted
                : MwColors.DisabledText,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );

        DrawChevron(graphics, chevronRect, Enabled ? MwColors.TextPrimary : MwColors.DisabledText);

        graphics.DrawPath(borderPen, path);
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

    private static int ClampInt(int value, int min, int max)
    {
        if (value < min)
            return min;

        if (value > max)
            return max;

        return value;
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

        TextRenderer.DrawText(
            graphics,
            footerText,
            MwFonts.Small,
            footerRect,
            !string.IsNullOrWhiteSpace(ErrorText)
                ? MwColors.Danger
                : MwColors.TextSecondary,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis
        );
    }

    private static void DrawChevron(Graphics graphics, Rectangle rect, Color color)
    {
        int centerX = rect.Left + rect.Width / 2;
        int centerY = rect.Top + rect.Height / 2;

        using var pen = new Pen(color, 2)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };

        graphics.DrawLine(pen, centerX - 6, centerY - 3, centerX, centerY + 3);
        graphics.DrawLine(pen, centerX, centerY + 3, centerX + 6, centerY - 3);
    }

    private static void DrawIcon(Graphics graphics, Rectangle rect, Color color)
    {
        DrawPrinterIcon(graphics, rect, color);
    }

    private static void DrawPrinterIcon(Graphics graphics, Rectangle rect, Color color)
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        float stroke = Math.Max(1.6f, rect.Width / 11f);

        using var pen = new Pen(color, stroke)
        {
            LineJoin = LineJoin.Round,
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };

        float x = rect.X + stroke;
        float y = rect.Y + stroke;
        float w = rect.Width - (stroke * 2);
        float h = rect.Height - (stroke * 2);

        // Üst kağıt
        var paperRect = new RectangleF(
            x + w * 0.24f,
            y + h * 0.02f,
            w * 0.52f,
            h * 0.28f
        );

        // Yazıcı gövdesi
        var bodyRect = new RectangleF(
            x + w * 0.10f,
            y + h * 0.32f,
            w * 0.80f,
            h * 0.38f
        );

        // Çıkan kağıt
        var outputRect = new RectangleF(
            x + w * 0.25f,
            y + h * 0.62f,
            w * 0.50f,
            h * 0.30f
        );

        graphics.DrawRectangle(pen, paperRect.X, paperRect.Y, paperRect.Width, paperRect.Height);
        graphics.DrawRectangle(pen, bodyRect.X, bodyRect.Y, bodyRect.Width, bodyRect.Height);
        graphics.DrawRectangle(pen, outputRect.X, outputRect.Y, outputRect.Width, outputRect.Height);

        using var dotBrush = new SolidBrush(color);

        float dotSize = Math.Max(2.2f, rect.Width / 9f);

        graphics.FillEllipse(
            dotBrush,
            bodyRect.Right - dotSize - stroke,
            bodyRect.Top + dotSize,
            dotSize,
            dotSize
        );
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

    private Rectangle GetInputRectangle()
    {
        int top = string.IsNullOrWhiteSpace(LabelText) ? 0 : LabelAreaHeight;

        return new Rectangle(
            1,
            top + 1,
            Width - 3,
            InputHeight - 3
        );
    }

    private string GetDisplayText()
    {
        if (SelectedIndex < 0 || SelectedIndex >= _items.Count)
            return string.Empty;

        return _items[SelectedIndex]?.ToString() ?? string.Empty;
    }

    private Color GetInputBackgroundColor()
    {
        if (!Enabled)
            return MwColors.DisabledBackground;

        if (_isPressed)
            return Color.FromArgb(249, 250, 251);

        return MwColors.Surface;
    }

    private Color GetBorderColor()
    {
        if (!Enabled)
            return MwColors.Border;

        if (!string.IsNullOrWhiteSpace(ErrorText))
            return MwColors.Danger;

        if (_isFocused)
            return MwColors.Primary;

        if (_isHovered)
            return Color.FromArgb(203, 213, 225);

        return Color.FromArgb(209, 217, 230);
    }

    private float GetBorderWidth()
    {
        if (_isFocused)
            return 1.6f;

        return 1f;
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