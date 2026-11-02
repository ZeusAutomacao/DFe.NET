using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    class BlocoCalculoImposto : BlocoBase
    {
        public BlocoCalculoImposto(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = ViewModel.CalculoImposto;
            if (ViewModel.ExibirTributosRtc)
            {
                AdicionarLinhaCampos()
                    .ComCampoNumerico("BASE DE CÁLCULO DO ICMS", m.BaseCalculoIcms)
                    .ComCampoNumerico("VALOR DO ICMS", m.ValorIcms)
                    .ComCampoNumerico("BASE DE CÁLCULO DO ICMS ST", m.BaseCalculoIcmsSt)
                    .ComCampoNumerico("VALOR DO ICMS ST", m.ValorIcmsSt)
                    .ComCampoNumerico("VALOR DO IPI", m.ValorIpi)
                    .ComLargurasIguais();
                if (ViewModel.ExibirIcmsInterestadual && (m.vICMSUFDest.HasValue || m.vFCPUFDest.HasValue))
                    AdicionarLinhaCampos()
                        .ComCampoNumerico("VALOR DO DIFAL NA UF DE DESTINO", m.vICMSUFDest)
                        .ComCampoNumerico("VALOR DO FCP NA UF DE DESTINO", m.vFCPUFDest)
                        .ComLargurasIguais();
                return;
            }

            var l = AdicionarLinhaCampos()
            .ComCampoNumerico("BASE DE CÁLC. DO ICMS", m.BaseCalculoIcms)
            .ComCampoNumerico("VALOR DO ICMS", m.ValorIcms)
            .ComCampoNumerico("BASE DE CÁLC. ICMS S.T.", m.BaseCalculoIcmsSt)
            .ComCampoNumerico("VALOR DO ICMS SUBST.", m.ValorIcmsSt)
            .ComCampoNumerico("V. IMP. IMPORTAÇÃO", m.ValorII);

            if (ViewModel.ExibirIcmsInterestadual)
            {
                l.ComCampoNumerico("V. ICMS UF REMET.", m.vICMSUFRemet)
                 .ComCampoNumerico("VALOR DO FCP", m.vFCPUFDest);
            }

            if (ViewModel.ExibirPisConfins)
            {
                l.ComCampoNumerico("VALOR DO PIS", m.ValorPis);
            }

            l.ComCampoNumerico("V. TOTAL PRODUTOS", m.ValorTotalProdutos)
           .ComLargurasIguais();

            l = AdicionarLinhaCampos()
            .ComCampoNumerico("Valor do Frete", m.ValorFrete)
            .ComCampoNumerico("Valor do Seguro", m.ValorSeguro)
            .ComCampoNumerico("Desconto", m.Desconto)
            .ComCampoNumerico("Outras Despesas", m.OutrasDespesas)
            .ComCampoNumerico("Valor Ipi", m.ValorIpi);

            if (ViewModel.ExibirIcmsInterestadual)
            {
                l.ComCampoNumerico("V. ICMS UF DEST.", m.vICMSUFDest)
                .ComCampoNumerico("V. TOT. TRIB.", m.ValorAproximadoTributos);
            }

            if (ViewModel.ExibirPisConfins)
            {
                l.ComCampoNumerico("VALOR DO COFINS", m.ValorCofins);
            }

            l.ComCampoNumerico("Valor Total da Nota", m.ValorTotalNota)
            .ComLargurasIguais();
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => ViewModel.ExibirTributosRtc ? "TOTAL DO ICMS / IPI" : "Cálculo do Imposto";
    }
}
