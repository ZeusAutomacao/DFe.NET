using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Elementos
{
    class TextoSimples : ElementoBase
    {
        public string Texto { get; private set; }
        public AlinhamentoHorizontal AlinhamentoHorizontal { get; set; }
        public AlinhamentoVertical AlinhamentoVertical { get; set; }
        public float TamanhoFonte { get; set; }

        public TextoSimples(Estilo estilo, string texto) : base(estilo)
        {
            Texto = texto;
            AlinhamentoHorizontal = AlinhamentoHorizontal.Esquerda;
            AlinhamentoVertical = AlinhamentoVertical.Topo;
            TamanhoFonte = 6;

        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            if (!string.IsNullOrWhiteSpace(Texto))
            {
                var r = BoundingBox.InflatedRetangle(0.75F);

                var tb = new TextBlock(Texto, Estilo.CriarFonteRegular(TamanhoFonte));
                tb.AlinhamentoHorizontal = AlinhamentoHorizontal;
                tb.Width = r.Width;

                var y = r.Y;

                if (AlinhamentoVertical == AlinhamentoVertical.Centro)
                    y += (r.Height - tb.Height) / 2F;

                tb.SetPosition(r.X, y);
                tb.Draw(gfx);
            }

        }


    }
}
