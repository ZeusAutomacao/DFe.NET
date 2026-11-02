// Copyright (c) colaboradores do DFe.NET. Licença LGPL-2.1-or-later; consulte LICENSE.
using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    /// <summary>Bloco de totais do DANFE conforme a NT 2026.010.</summary>
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
