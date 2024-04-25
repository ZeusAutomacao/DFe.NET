using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Define um bloco de texto de largura fixa e altura dinâmica.
    /// </summary>
    internal class TextBlock : DrawableBase
    {
        private string _Text;
        public string Text { get => _Text; set { _Text = value; _Height = null; } }

        public List<string[]> Blocks { get; private set; }
        public List<float[]> BlocksW { get; private set; }

        public Fonte Fonte { get; private set; }

        public List<string> Lines { get; private set; }
        private float? _Height;

        public override float Width { get => base.Width; set { base.Width = value; _Height = null; } }
        public AlinhamentoHorizontal AlinhamentoHorizontal { get; set; }

        public override float Height
        {
            get
            {
                if (!_Height.HasValue) CalculateLines();
                return _Height.Value;
            }
            set => throw new NotSupportedException();
        }



        public override void Draw(Gfx gfx)
        {
            if (string.IsNullOrEmpty(_Text)) return;

            base.Draw(gfx);
            if (!_Height.HasValue) CalculateLines();

            float y = Y;

            foreach (var item in Lines)
            {
                gfx.DrawString(item, new RectangleF(X, y, Width, _Height.Value), Fonte, AlinhamentoHorizontal, AlinhamentoVertical.Topo);
                y += Fonte.AlturaLinha;
            }
        }

        private void BreakLongText(string str)
        {
            char[] c = str.ToCharArray();
            float w1 = 0;
            StringBuilder sb1 = new StringBuilder();

            for (int i2 = 0; i2 < c.Length; i2++)
            {
                float cw = Fonte.MedirLarguraChar(c[i2]);
                if (cw + w1 > Width)
                {
                    Lines.Add(sb1.ToString());
                    sb1.Clear();
                    w1 = 0;
                }

                w1 += cw;
                sb1.Append(c[i2]);
            }

            Lines.Add(sb1.ToString());
        }

        private void CalculateBlocks(string[] blocks, float[] blocksW)
        {
            float x = 0;
            StringBuilder sb = new StringBuilder();

            int i = 0;

            while (i < blocks.Length)
            {
                var w = blocks[i];
                var wl = blocksW[i];

                if (wl > Width)
                {

                    if (sb.Length > 0)
                    {
                        Lines.Add(sb.ToString());
                        sb.Clear();
                    }

                    BreakLongText(w);
                    i++;
                    x = 0;
                }
                else if (x + wl <= Width)
                {
                    x += wl;
                    sb.Append(w);
                    i++;
                }
                else
                {
                    if (w == " ") i++;

                    x = 0;
                    Lines.Add(sb.ToString().TrimEnd());
                    sb.Clear();
                }
            }

            if (sb.Length > 0) Lines.Add(sb.ToString());
        }

        private void CalculateLines()
        {
            Lines.Clear();

            for (int i = 0; i < Blocks.Count; i++)
            {
                CalculateBlocks(Blocks[i], BlocksW[i]);
            }

            _Height = Lines.Count * Fonte.AlturaLinha;
        }

        private void SplitText()
        {
            var lines = Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                var blocks = Regex.Split(line, @"(\s)");
                var blocksW = new float[blocks.Length];

                for (int i2 = 0; i2 < blocks.Length; i2++)
                {
                    blocksW[i2] = Fonte.MedirLarguraTexto(blocks[i2]);
                }

                Blocks.Add(blocks);
                BlocksW.Add(blocksW);
            }
        }

        public TextBlock(string text, Fonte f)
        {
            Text = text;
            Fonte = f;
            Blocks = new List<string[]>();
            BlocksW = new List<float[]>();
            SplitText();
            AlinhamentoHorizontal = AlinhamentoHorizontal.Esquerda;
            Lines = new List<string>();
        }
    }
}
