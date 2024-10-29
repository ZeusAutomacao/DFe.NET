using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Status;
using CTe.Servicos.Factory;
using CTe.Utils.Extencoes;

namespace CTe.Servicos.ConsultaStatus
{
    public class StatusServico
    {
        public retConsStatServCte ConsultaStatus(ConfiguracaoServico configuracaoServico = null)
        {
            var consStatServCte = ClassesFactory.CriaConsStatServCte(configuracaoServico);
            
            if (configuracaoServico.IsValidaSchemas)
                consStatServCte.ValidarSchema(configuracaoServico);
            
            consStatServCte.SalvarXmlEmDisco(configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteStatusServico(configuracaoServico);
            var retornoXml = webService.cteStatusServicoCT(consStatServCte.CriaRequestWs());

            var retorno = retConsStatServCte.LoadXml(retornoXml.OuterXml, consStatServCte);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }

        public retConsStatServCTe ConsultaStatusV4(ConfiguracaoServico configuracaoServico = null)
        {
            var consStatServCte = ClassesFactory.CriaConsStatServCTe(configuracaoServico);

            if (configuracaoServico.IsValidaSchemas)
                consStatServCte.ValidarSchema(configuracaoServico);
            
            consStatServCte.SalvarXmlEmDisco(configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteStatusServico(configuracaoServico);
            var retornoXml = webService.cteStatusServicoCT(consStatServCte.CriaRequestWs());

            var retorno = retConsStatServCTe.LoadXml(retornoXml.OuterXml, consStatServCte);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }

        public async Task<retConsStatServCte> ConsultaStatusAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var consStatServCte = ClassesFactory.CriaConsStatServCte(configuracaoServico);

            if (configuracaoServico.IsValidaSchemas)
                consStatServCte.ValidarSchema(configuracaoServico);
            
            consStatServCte.SalvarXmlEmDisco(configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteStatusServico(configuracaoServico);
            var retornoXml = await webService.cteStatusServicoCTAsync(consStatServCte.CriaRequestWs());

            var retorno = retConsStatServCte.LoadXml(retornoXml.OuterXml, consStatServCte);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }
    }
}