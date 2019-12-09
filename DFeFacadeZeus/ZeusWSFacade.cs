using DFeFacadeBase;
using NFe.Servicos;
using NFe.Utils;

namespace DFeFacadeZeus
{
    public class ZeusWSFacade : IWSFacade<ConfiguracaoServico>
    {
        public IConsultaStatusRetorno ConsultaStatus(DFeBase<ConfiguracaoServico> dfeBase)
        {
            using (ServicosNFe servicosNFe = new ServicosNFe(dfeBase.ObterConfiguracao()))
            {
                var retorno = servicosNFe.NfeStatusServico();
            }

            return null;
        }
    }
}