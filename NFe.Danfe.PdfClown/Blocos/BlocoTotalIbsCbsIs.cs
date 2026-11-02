using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class BlocoTotalIbsCbsIs : BlocoBase
    {
        public BlocoTotalIbsCbsIs(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = viewModel.CalculoImposto;
            var linha = AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR DA CBS", (double?)m.ValorCbs)
                .ComCampoNumerico("VALOR DO IBS UF", (double?)m.ValorIbsUf)
                .ComCampoNumerico("VALOR DO IBS MUNICÍPIO", (double?)m.ValorIbsMunicipio)
                .ComCampoNumerico("VALOR DO IMPOSTO SELETIVO", (double?)m.ValorImpostoSeletivo)
                .ComLargurasIguais();

            if (m.ValorIbsMono.HasValue || m.ValorCbsMono.HasValue ||
                m.ValorIbsMonoReten.HasValue || m.ValorCbsMonoReten.HasValue)
            {
                AdicionarLinhaCampos()
                    .ComCampoNumerico("VALOR DO IBS MONOFÁSICO", (double?)m.ValorIbsMono)
                    .ComCampoNumerico("VALOR DA CBS MONOFÁSICA", (double?)m.ValorCbsMono)
                    .ComCampoNumerico("IBS MONOFÁSICO POR RETENÇÃO", (double?)m.ValorIbsMonoReten)
                    .ComCampoNumerico("CBS MONOFÁSICA POR RETENÇÃO", (double?)m.ValorCbsMonoReten)
                    .ComLargurasIguais();
            }
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "Total do IBS/CBS/IS";
    }
}
