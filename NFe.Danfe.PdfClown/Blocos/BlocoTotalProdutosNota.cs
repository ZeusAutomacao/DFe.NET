using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class BlocoTotalProdutosNota : BlocoBase
    {
        public BlocoTotalProdutosNota(DanfeViewModel model, Estilo estilo) : base(model, estilo)
        {
            var m = model.CalculoImposto;
            AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR TOTAL DOS PRODUTOS", m.ValorTotalProdutos)
                .ComCampoNumerico("VALOR DO FRETE", m.ValorFrete)
                .ComCampoNumerico("VALOR DO SEGURO", m.ValorSeguro)
                .ComCampoNumerico("DESCONTO", m.Desconto)
                .ComCampoNumerico("OUTRAS DESPESAS", m.OutrasDespesas)
                .ComLargurasIguais();
            AdicionarLinhaCampos().ComCampoNumerico("VALOR TOTAL DA NOTA", m.ValorTotalNota).ComLargurasIguais();
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "TOTAL DOS PRODUTOS E TOTAL DA NOTA";
    }
}
