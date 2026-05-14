# ModernWinFormsUI

ModernWinFormsUI is a lightweight modern UI component library for building cleaner and more consistent .NET 8 WinForms applications.

![ModernWinFormsUI Demo](docs/screenshots/demo-overview.png)

## Features

- Modern WinForms controls
- Designer-friendly component usage
- Theme tokens for colors, fonts, spacing and radius
- Custom button, card, text box, badge and alert components
- Lightweight and dependency-free
- Built for .NET 8 Windows Forms
- Segmented button component for selection-based UI patterns

## Components

- `MwButton`
- `MwCard`
- `MwTextBox`
- `MwBadge`
- `MwAlert`
- `MwSegmentedButton`

## Quick Example

```csharp
using ModernWinFormsUI.Controls;

var button = new MwButton
{
    Text = "Save",
    Variant = MwButtonVariant.Primary,
    ButtonSize = MwButtonSize.Medium,
    Width = 120
};

var segmentedButton = new MwSegmentedButton
{
    Text = "TC / Öğrenci No",
    IconText = "●",
    Selected = true,
    Width = 180
};

Project Status

This project is currently in early development.

Current version:

v0.1.0
Roadmap
Add MwComboBox
Add MwStatusBar
Add MwDataGridViewStyler
Add dark theme support
Publish as a NuGet package
Improve documentation and examples
License

MIT License