using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Elementos
{
    class NumeroNfSerie : ElementoBase
    {
        public string NfNumero { get; private set; }
        public string NfSerie { get; private set; }

        public NumeroNfSerie(Estilo estilo, string nfNumero, string nfSerie) : base(estilo)
        {
            NfNumero = nfNumero;
            NfSerie = nfSerie;
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            var r = BoundingBox.InflatedRetangle(1);

            var f1 = Estilo.CriarFonteNegrito(14);
            var f2 = Estilo.CriarFonteNegrito(11F);

            gfx.DrawString("NF-e", r, f1, AlinhamentoHorizontal.Centro);

            r = r.CutTop(f1.AlturaLinha);

            TextStack ts = new TextStack(r)
            {
                AlinhamentoHorizontal = AlinhamentoHorizontal.Centro,
                AlinhamentoVertical = AlinhamentoVertical.Centro,
                LineHeightScale = 1F
            }
            .AddLine($"Nº.: {NfNumero}", f2)
            .AddLine($"Série: {NfSerie}", f2);

            ts.Draw(gfx);

        }
    }
}
