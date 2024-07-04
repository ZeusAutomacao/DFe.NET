using System.Drawing;
using NFe.Danfe.PdfClown.Graphics;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Define um objeto desenhável.
    /// </summary>
    internal abstract class DrawableBase
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        public virtual float Width { get; set; }
        public virtual float Height { get; set; }

        public PointF Position => new PointF(X, Y);
        public SizeF Size => new SizeF(Width, Height);

        public DrawableBase()
        {
        }

        public virtual void Draw(Gfx gfx)
        {
            if (gfx == null) throw new ArgumentNullException(nameof(gfx));
            if (Width <= 0) throw new InvalidOperationException("Width is invalid.");
            if (Height <= 0) throw new InvalidOperationException("Height is invalid.");
            if (X < 0) throw new InvalidOperationException("X is invalid.");
            if (Y < 0) throw new InvalidOperationException("X is invalid.");
        }

        public virtual void SetPosition(float x, float y)
        {
            X = x;
            Y = y;
        }

        public virtual void SetPosition(PointF p) => SetPosition(p.X, p.Y);

        public virtual void SetSize(float w, float h)
        {
            Width = w;
            if (Height != h)
                Height = h;
        }
        public virtual void SetSize(SizeF s) => SetSize(s.Width, s.Height);

        public RectangleF BoundingBox => new RectangleF(X, Y, Width, Height);
    }
}
