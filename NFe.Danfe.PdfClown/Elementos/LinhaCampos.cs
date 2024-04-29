using NFe.Danfe.PdfClown.Enumeracoes;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Linha de campos, posiciona e muda a largura desses elementos de forma proporcional.
    /// </summary>
    internal class LinhaCampos : FlexibleLine
    {
        public Estilo Estilo { get; private set; }

        public LinhaCampos(Estilo estilo, float width, float height = Constantes.CampoAltura) : base()
        {
            Estilo = estilo;
            SetSize(width, height);
        }

        public LinhaCampos(Estilo estilo) : base()
        {
            Estilo = estilo;
        }

        public virtual LinhaCampos ComCampo(string cabecalho, string conteudo, AlinhamentoHorizontal alinhamentoHorizontalConteudo = AlinhamentoHorizontal.Esquerda)
        {
            var campo = new Campo(cabecalho, conteudo, Estilo, alinhamentoHorizontalConteudo);
            Elementos.Add(campo);
            return this;
        }

        public virtual LinhaCampos ComCampoNumerico(string cabecalho, double? conteudoNumerico, int casasDecimais = 2)
        {
            var campo = new CampoNumerico(cabecalho, conteudoNumerico, Estilo, casasDecimais);
            Elementos.Add(campo);
            return this;
        }
    }
}
