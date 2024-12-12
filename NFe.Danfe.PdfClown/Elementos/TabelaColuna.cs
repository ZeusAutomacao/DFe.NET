using NFe.Danfe.PdfClown.Enumeracoes;

namespace NFe.Danfe.PdfClown.Elementos
{
    internal class TabelaColuna
    {
        public string[] Cabecalho { get; private set; }
        public float PorcentagemLargura { get; set; }
        public AlinhamentoHorizontal AlinhamentoHorizontal { get; private set; }

        public TabelaColuna(string[] cabecalho, float porcentagemLargura, AlinhamentoHorizontal alinhamentoHorizontal = AlinhamentoHorizontal.Esquerda)
        {
            Cabecalho = cabecalho ?? throw new ArgumentNullException(nameof(cabecalho));
            PorcentagemLargura = porcentagemLargura;
            AlinhamentoHorizontal = alinhamentoHorizontal;
        }

        public override string ToString()
        {
            return string.Join(" ", Cabecalho);
        }
    }
}
