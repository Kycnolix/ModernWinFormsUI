using System.Drawing;

namespace ModernWinFormsUI.Tokens;

public static class MwColors
{
    // App surfaces
    public static readonly Color Page = Color.FromArgb(245, 247, 250);
    public static readonly Color Surface = Color.FromArgb(255, 255, 255);
    public static readonly Color SurfaceSoft = Color.FromArgb(249, 250, 251);

    // Borders
    public static readonly Color Border = Color.FromArgb(218, 224, 235);
    public static readonly Color BorderStrong = Color.FromArgb(156, 163, 175);

    // Text
    public static readonly Color TextPrimary = Color.FromArgb(17, 24, 39);
    public static readonly Color TextSecondary = Color.FromArgb(75, 85, 99);
    public static readonly Color TextMuted = Color.FromArgb(107, 114, 128);
    public static readonly Color TextInverse = Color.FromArgb(255, 255, 255);

    // Primary action
    public static readonly Color Primary = Color.FromArgb(37, 99, 235);
    public static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);
    public static readonly Color PrimaryPressed = Color.FromArgb(30, 64, 175);
    public static readonly Color PrimarySoft = Color.FromArgb(239, 246, 255);

    // Feedback
    public static readonly Color Success = Color.FromArgb(22, 163, 74);
    public static readonly Color SuccessSoft = Color.FromArgb(240, 253, 244);

    public static readonly Color Warning = Color.FromArgb(217, 119, 6);
    public static readonly Color WarningSoft = Color.FromArgb(255, 251, 235);

    public static readonly Color Danger = Color.FromArgb(220, 38, 38);
    public static readonly Color DangerHover = Color.FromArgb(185, 28, 28);
    public static readonly Color DangerPressed = Color.FromArgb(153, 27, 27);
    public static readonly Color DangerSoft = Color.FromArgb(254, 242, 242);

    public static readonly Color Info = Color.FromArgb(2, 132, 199);
    public static readonly Color InfoSoft = Color.FromArgb(240, 249, 255);

    // Disabled
    public static readonly Color DisabledBackground = Color.FromArgb(229, 231, 235);
    public static readonly Color DisabledText = Color.FromArgb(156, 163, 175);
}