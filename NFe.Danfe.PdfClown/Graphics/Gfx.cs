using System.Drawing;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Tools;
using org.pdfclown.documents.contents.composition;
using org.pdfclown.documents.contents.xObjects;

namespace NFe.Danfe.PdfClown.Graphics
{
    internal class Gfx
    {
        public PrimitiveComposer PrimitiveComposer { get; private set; }

        public Gfx(PrimitiveComposer primitiveComposer)
        {
            PrimitiveComposer = primitiveComposer ?? throw new ArgumentNullException(nameof(primitiveComposer));
        }

        internal void DrawString(string str, RectangleF rect, Fonte fonte, AlinhamentoHorizontal ah = AlinhamentoHorizontal.Esquerda, AlinhamentoVertical av = AlinhamentoVertical.Topo)
        {
            if (fonte == null) throw new ArgumentNullException(nameof(fonte));
            if (fonte.Tamanho <= 0) throw new ArgumentOutOfRangeException(nameof(fonte));
            CheckRectangle(rect);

            var p = rect.Location;

            if (av == AlinhamentoVertical.Base)
                p.Y = rect.Bottom - fonte.AlturaLinha;
            else if (av == AlinhamentoVertical.Centro)
                p.Y += (rect.Height - fonte.AlturaLinha) / 2F;

            if (ah == AlinhamentoHorizontal.Direita)
                p.X = rect.Right - fonte.MedirLarguraTexto(str);
            if (ah == AlinhamentoHorizontal.Centro)
                p.X += (rect.Width - fonte.MedirLarguraTexto(str)) / 2F;

            SetFont(fonte);
            ShowText(str, p);
        }

        public void SetFont(Fonte fonte)
        {
            if (fonte == null) throw new ArgumentNullException(nameof(fonte));
            if (fonte.FonteInterna == null) throw new ArgumentNullException(nameof(fonte));
            if (fonte.Tamanho <= 0) throw new ArgumentNullException(nameof(fonte));
            PrimitiveComposer.SetFont(fonte.FonteInterna, fonte.Tamanho);
        }

        public void ShowText(string text, PointF point)
        {
            CheckPoint(point);
            PrimitiveComposer.ShowText(text, point.ToPointMeasure());
        }

        public void ShowXObject(XObject xobj, RectangleF r)
        {
            if (xobj == null) throw new ArgumentNullException(nameof(xobj));
            CheckRectangle(r);

            PointF p = new PointF();
            SizeF s = new SizeF();
            SizeF xs = xobj.Size.ToMm();

            if (r.Height >= r.Width)
            {
                if (xs.Height >= xs.Width)
                {
                    s.Height = r.Height;
                    s.Width = (s.Height * xs.Width) / xs.Height;
                }
                else
                {
                    s.Width = r.Width;
                    s.Height = (s.Width * xs.Height) / xs.Width;
                }
            }
            else
            {
                if (xs.Height >= xs.Width)
                {
                    s.Width = r.Width;
                    s.Height = (s.Width * xs.Height) / xs.Width;
                }
                else
                {
                    s.Height = r.Height;
                    s.Width = (s.Height * xs.Width) / xs.Height;
                }
            }

            p.X = r.X + Math.Abs(r.Width - s.Width) / 2F;
            p.Y = r.Y + Math.Abs(r.Height - s.Height) / 2F;

            PrimitiveComposer.ShowXObject(xobj, p.ToPointMeasure(), s.ToPointMeasure());
        }

        public void StrokeRectangle(RectangleF rect, float width)
        {
            SetLineWidth(width);
            DrawRectangle(rect);
            Stroke();
        }

        public void SetLineWidth(float w)
        {
            if (w < 0) throw new ArgumentOutOfRangeException(nameof(w));
            PrimitiveComposer.SetLineWidth(w);
        }

        public void DrawRectangle(RectangleF rect)
        {
            CheckRectangle(rect);
            PrimitiveComposer.DrawRectangle(rect.ToPointMeasure());
        }

        private void CheckRectangle(RectangleF r)
        {
            if (r.X < 0 || r.Y < 0 || r.Width <= 0 || r.Height <= 0) throw new ArgumentException(nameof(r));
        }

        private void CheckPoint(PointF p)
        {
            if (p.X < 0 || p.Y < 0) throw new ArgumentException(nameof(p));
        }

        public void Stroke() => PrimitiveComposer.Stroke();
        public void Flush() => PrimitiveComposer.Flush();
        public void Fill() => PrimitiveComposer.Fill();
        public void DrawRectangle(float x, float y, float w, float h) => DrawRectangle(new RectangleF(x, y, w, h));


    }
}
