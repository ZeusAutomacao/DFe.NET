using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Cabeçalho do bloco, normalmente um texto em caixa alta.
    /// </summary>
    internal class CabecalhoBloco : ElementoBase
    {
        public const float MargemSuperior = 0.8F;
        public string Cabecalho { get; set; }

        public CabecalhoBloco(Estilo estilo, string cabecalho) : base(estilo)
        {
            Cabecalho = cabecalho ?? throw new ArgumentNullException(cabecalho);
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            gfx.DrawString(Cabecalho.ToUpper(), BoundingBox, Estilo.FonteBlocoCabecalho,
                AlinhamentoHorizontal.Esquerda, AlinhamentoVertical.Base);
        }

        public override float Height { get => MargemSuperior + Estilo.FonteBlocoCabecalho.AlturaLinha; set => throw new NotSupportedException(); }
        public override bool PossuiContono => false;
    }
}
