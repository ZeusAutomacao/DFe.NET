using DFeFacadeBase;
using NFe.Servicos;
using NFe.Utils;

namespace DFeFacadeZeus
{
    public class ZeusWSFacadeStatusServico : IWStatusServico<ConfiguracaoServico>
    {
        public IConsultaStatusRetorno ConsultaStatus(DFeBase<ConfiguracaoServico> dfeBase)
        {
            using (ServicosNFe servicosNFe = new ServicosNFe(dfeBase.ObterConfiguracao()))
            {
                var retorno = servicosNFe.NfeStatusServico();

                return new ConsultaStatusRetorno(
                    retorno.Retorno.versao,
                    (DFeAmbiente)retorno.Retorno.tpAmb,
                    retorno.Retorno.verAplic,
                    retorno.Retorno.cStat.ToString(),
                    retorno.Retorno.xMotivo,
                    (DFeEstado)retorno.Retorno.cUF,
                    retorno.Retorno.dhRecbto, retorno.Retorno.dhRetorno,
                    retorno.Retorno.xObs,
                    retorno.Retorno.tMed,
                    retorno.EnvioStr,
                    retorno.RetornoCompletoStr,
                    retorno
                );
            }
        }
    }
}