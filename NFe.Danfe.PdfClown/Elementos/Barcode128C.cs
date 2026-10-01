using System.Drawing;
using System.Text.RegularExpressions;
using NFe.Danfe.PdfClown.Graphics;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Elementos
{
    internal class Barcode128C : ElementoBase
    {
        private static byte[][] Dic;

        public static readonly float MargemVertical = 2;

        /// <summary>
        /// Código a ser codificado em barras.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Largura do código de barras.
        /// </summary>
        public float Largura { get; set; }

        static Barcode128C()
        {

            Dic = new byte[][]
            {
                new byte[] { 2,1,2,2,2,2},
                new byte[] { 2,2,2,1,2,2},
                new byte[] { 2,2,2,2,2,1},
                new byte[] { 1,2,1,2,2,3},
                new byte[] { 1,2,1,3,2,2},
                new byte[] { 1,3,1,2,2,2},
                new byte[] { 1,2,2,2,1,3},
                new byte[] { 1,2,2,3,1,2},
                new byte[] { 1,3,2,2,1,2},
                new byte[] { 2,2,1,2,1,3},
                new byte[] { 2,2,1,3,1,2},
                new byte[] { 2,3,1,2,1,2},
                new byte[] { 1,1,2,2,3,2},
                new byte[] { 1,2,2,1,3,2},
                new byte[] { 1,2,2,2,3,1},
                new byte[] { 1,1,3,2,2,2},
                new byte[] { 1,2,3,1,2,2},
                new byte[] { 1,2,3,2,2,1},
                new byte[] { 2,2,3,2,1,1},
                new byte[] { 2,2,1,1,3,2},
                new byte[] { 2,2,1,2,3,1},
                new byte[] { 2,1,3,2,1,2},
                new byte[] { 2,2,3,1,1,2},
                new byte[] { 3,1,2,1,3,1},
                new byte[] { 3,1,1,2,2,2},
                new byte[] { 3,2,1,1,2,2},
                new byte[] { 3,2,1,2,2,1},
                new byte[] { 3,1,2,2,1,2},
                new byte[] { 3,2,2,1,1,2},
                new byte[] { 3,2,2,2,1,1},
                new byte[] { 2,1,2,1,2,3},
                new byte[] { 2,1,2,3,2,1},
                new byte[] { 2,3,2,1,2,1},
                new byte[] { 1,1,1,3,2,3},
                new byte[] { 1,3,1,1,2,3},
                new byte[] { 1,3,1,3,2,1},
                new byte[] { 1,1,2,3,1,3},
                new byte[] { 1,3,2,1,1,3},
                new byte[] { 1,3,2,3,1,1},
                new byte[] { 2,1,1,3,1,3},
                new byte[] { 2,3,1,1,1,3},
                new byte[] { 2,3,1,3,1,1},
                new byte[] { 1,1,2,1,3,3},
                new byte[] { 1,1,2,3,3,1},
                new byte[] { 1,3,2,1,3,1},
                new byte[] { 1,1,3,1,2,3},
                new byte[] { 1,1,3,3,2,1},
                new byte[] { 1,3,3,1,2,1},
                new byte[] { 3,1,3,1,2,1},
                new byte[] { 2,1,1,3,3,1},
                new byte[] { 2,3,1,1,3,1},
                new byte[] { 2,1,3,1,1,3},
                new byte[] { 2,1,3,3,1,1},
                new byte[] { 2,1,3,1,3,1},
                new byte[] { 3,1,1,1,2,3},
                new byte[] { 3,1,1,3,2,1},
                new byte[] { 3,3,1,1,2,1},
                new byte[] { 3,1,2,1,1,3},
                new byte[] { 3,1,2,3,1,1},
                new byte[] { 3,3,2,1,1,1},
                new byte[] { 3,1,4,1,1,1},
                new byte[] { 2,2,1,4,1,1},
                new byte[] { 4,3,1,1,1,1},
                new byte[] { 1,1,1,2,2,4},
                new byte[] { 1,1,1,4,2,2},
                new byte[] { 1,2,1,1,2,4},
                new byte[] { 1,2,1,4,2,1},
                new byte[] { 1,4,1,1,2,2},
                new byte[] { 1,4,1,2,2,1},
                new byte[] { 1,1,2,2,1,4},
                new byte[] { 1,1,2,4,1,2},
                new byte[] { 1,2,2,1,1,4},
                new byte[] { 1,2,2,4,1,1},
                new byte[] { 1,4,2,1,1,2},
                new byte[] { 1,4,2,2,1,1},
                new byte[] { 2,4,1,2,1,1},
                new byte[] { 2,2,1,1,1,4},
                new byte[] { 4,1,3,1,1,1},
                new byte[] { 2,4,1,1,1,2},
                new byte[] { 1,3,4,1,1,1},
                new byte[] { 1,1,1,2,4,2},
                new byte[] { 1,2,1,1,4,2},
                new byte[] { 1,2,1,2,4,1},
                new byte[] { 1,1,4,2,1,2},
                new byte[] { 1,2,4,1,1,2},
                new byte[] { 1,2,4,2,1,1},
                new byte[] { 4,1,1,2,1,2},
                new byte[] { 4,2,1,1,1,2},
                new byte[] { 4,2,1,2,1,1},
                new byte[] { 2,1,2,1,4,1},
                new byte[] { 2,1,4,1,2,1},
                new byte[] { 4,1,2,1,2,1},
                new byte[] { 1,1,1,1,4,3},
                new byte[] { 1,1,1,3,4,1},
                new byte[] { 1,3,1,1,4,1},
                new byte[] { 1,1,4,1,1,3},
                new byte[] { 1,1,4,3,1,1},
                new byte[] { 4,1,1,1,1,3},
                new byte[] { 4,1,1,3,1,1},
                new byte[] { 1,1,3,1,4,1},
                new byte[] { 1,1,4,1,3,1},
                new byte[] { 3,1,1,1,4,1},
                new byte[] { 4,1,1,1,3,1},
                new byte[] { 2,1,1,4,1,2},
                new byte[] { 2,1,1,2,1,4},
                new byte[] { 2,1,1,2,3,2},
                new byte[] { 2,3,3,1,1,1,2}

            };

        }

        public Barcode128C(string code, Estilo estilo, float largura = 75F) : base(estilo)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("O código não pode ser vazio.", "code");
            }

            // NT Conjunta 2025.001: a chave de acesso pode conter letras maiúsculas nas posições do CNPJ
            if (!Regex.IsMatch(code, "^[0-9A-Z]+$"))
            {
                throw new ArgumentException("O código deve apenas conter digítos numéricos e letras maiúsculas.", "code");
            }

            if (Regex.IsMatch(code, @"^\d+$") && code.Length % 2 != 0)
            {
                Code = "0" + code;
            }
            else
            {
                Code = code;
            }

            Largura = largura;
        }

        private void DrawBarcode(RectangleF rect, Gfx gfx)
        {
            byte[] codeBytes = Code128Hibrido.ObterSimbolos(this.Code);

            float n = codeBytes.Length * 11 + 2;
            float w = rect.Width / n;

            float x = 0;

            for (int i = 0; i < codeBytes.Length; i++)
            {
                byte[] pt = Barcode128C.Dic[codeBytes[i]];

                for (int i2 = 0; i2 < pt.Length; i2++)
                {
                    if (i2 % 2 == 0)
                    {
                        gfx.DrawRectangle(rect.X + x, rect.Y, w * pt[i2], rect.Height);
                    }

                    x += w * pt[i2];
                }
            }

            gfx.Fill();
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            float w2 = (Width - Largura) / 2F;
            DrawBarcode(new RectangleF(X + w2, Y + MargemVertical, Largura, Height - 2 * MargemVertical), gfx);
        }
    }
}
