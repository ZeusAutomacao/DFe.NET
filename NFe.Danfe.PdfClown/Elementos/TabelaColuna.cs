using NFe.Danfe.PdfClown.Enumeracoes;

namespace NFe.Danfe.PdfClown.Elementos
{
    internal class TabelaColuna
    {
        public string[] Cabecalho { get; private set; }
        public float PorcentagemLargura { get; set; }
        public AlinhamentoHorizontal AlinhamentoHorizontal { get; private set; }

        /// <summary>
        ///     Indica que o conteúdo desta coluna é indivisível — um valor formatado, sem espaços.
        ///     Quando <c>true</c>, um conteúdo mais largo que a célula é impresso em fonte reduzida
        ///     em vez de ser quebrado no meio dos caracteres.
        /// </summary>
        public bool AjustarFonteParaCaber { get; private set; }

        public TabelaColuna(string[] cabecalho, float porcentagemLargura, AlinhamentoHorizontal alinhamentoHorizontal = AlinhamentoHorizontal.Esquerda, bool ajustarFonteParaCaber = false)
        {
            Cabecalho = cabecalho ?? throw new ArgumentNullException(nameof(cabecalho));
            PorcentagemLargura = porcentagemLargura;
            AlinhamentoHorizontal = alinhamentoHorizontal;
            AjustarFonteParaCaber = ajustarFonteParaCaber;
        }

        public override string ToString()
        {
            return string.Join(" ", Cabecalho);
        }
    }
}
