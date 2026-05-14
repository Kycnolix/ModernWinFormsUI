using System.Drawing;
using System.Drawing.Drawing2D;

namespace ModernWinFormsUI.Utilities;

public static class GraphicsHelper
{
    public static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;

        var path = new GraphicsPath();

        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            path.CloseFigure();
            return path;
        }

        if (diameter > bounds.Width)
            diameter = bounds.Width;

        if (diameter > bounds.Height)
            diameter = bounds.Height;

        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

        path.CloseFigure();

        return path;
    }
}