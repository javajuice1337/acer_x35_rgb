using System;
using System.Drawing;
using System.Windows.Forms;

namespace acer_rgb
{
    internal class ColorItem
    {
        public Color color;
        public String hex;
        public Bitmap img;

        public ColorItem(Color c)
        {
            Bitmap bitmap = new Bitmap(SystemInformation.SmallIconSize.Width, SystemInformation.SmallIconSize.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (Brush b = new SolidBrush(c))
                {
                    g.FillRectangle(b, 0, 0, bitmap.Width, bitmap.Height);
                }
            }

            String clr = String.Format("{0:X}", c.ToArgb());
            if (clr.Length > 2) clr = clr.Substring(2);
            clr = "#" + clr;

            color = c;
            hex = clr;
            img = bitmap;
        }
    }
}
