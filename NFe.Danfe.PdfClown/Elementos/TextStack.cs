using System.Drawing;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Define uma pilha de texto.
    /// </summary>
    internal class TextStack : DrawableBase
    {
        public const float DefaultLineHeightScale = 1.25F;

        private List<string> _Lines;
        private List<Fonte> _Fonts;
        public AlinhamentoHorizontal AlinhamentoHorizontal { get; set; }
        public AlinhamentoVertical AlinhamentoVertical { get; set; }
        public float LineHeightScale { get; set; }

        public TextStack(RectangleF boundingBox)
        {
            SetPosition(boundingBox.Location);
            SetSize(boundingBox.Size);
            _Lines = new List<string>();
            _Fonts = new List<Fonte>();
            AlinhamentoHorizontal = AlinhamentoHorizontal.Centro;
            AlinhamentoVertical = AlinhamentoVertical.Centro;
            LineHeightScale = DefaultLineHeightScale;
        }

        public TextStack AddLine(string text, Fonte font)
        {
            _Lines.Add(text);
            _Fonts.Add(font);
            return this;
        }

        public override void Draw(Gfx gfx)
        {
            var fonts = new Fonte[_Fonts.Count];

            //adjust font size to prevent horizontal overflown
            for (int i = 0; i < _Lines.Count; i++)
            {
                var w = _Fonts[i].MedirLarguraTexto(_Lines[i]);

                if (w > BoundingBox.Width)
                {
                    fonts[i] = new Fonte(_Fonts[i].FonteInterna, BoundingBox.Width * _Fonts[i].Tamanho / w);
                }
                else
                {
                    fonts[i] = _Fonts[i];
                }
            }

            float totalH = (float)fonts.Last().AlturaLinha;

            for (int i = 0; i < _Lines.Count - 1; i++)
            {
                totalH += (float)fonts[i].AlturaLinha * LineHeightScale;
            }

            // float totalH = (float)fonts.Sum(x => x.AlturaEmMm());
            var h2 = (BoundingBox.Height - totalH) / 2D;
            var r = BoundingBox;

            if (AlinhamentoVertical == AlinhamentoVertical.Centro)
                r.Y += (float)h2;
            else if (AlinhamentoVertical == AlinhamentoVertical.Base)
                r.Y = r.Bottom - totalH;

            for (int i = 0; i < _Lines.Count; i++)
            {
                var l = _Lines[i];
                var f = fonts[i];

                gfx.DrawString(l, r, f, AlinhamentoHorizontal);
                r.Y += f.AlturaLinha * LineHeightScale;
            }

        }
    }
}
