using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using CTe.Servicos.Factory;
using CTe.Utils.Extencoes;

namespace CTe.Servicos.ConsultaProtocolo
{
    public class ConsultaProtcoloServico
    {
        public retConsSitCTe ConsultaProtocolo(string chave, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var consSitCTe = ClassesFactory.CriarconsSitCTe(chave, configServico);

            if (configServico.IsValidaSchemas)
                consSitCTe.ValidarSchema(configServico);

            consSitCTe.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlConsultaProtocolo(configServico);
            var retornoXml = webService.cteConsultaCT(consSitCTe.CriaRequestWs());

            var retorno = retConsSitCTe.LoadXml(retornoXml.OuterXml, consSitCTe);
            retorno.SalvarXmlEmDisco(chave, configServico);

            return retorno;
        }

        public retConsSitCTe ConsultaProtocoloV4(string chave, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var consSitCTe = ClassesFactory.CriarconsSitCTe(chave, configServico);

            if (configServico.IsValidaSchemas)
                consSitCTe.ValidarSchema(configServico);

            consSitCTe.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlConsultaProtocoloV4(configServico);
            var retornoXml = webService.cteConsultaCT(consSitCTe.CriaRequestWs());

            var retorno = retConsSitCTe.LoadXml(retornoXml.OuterXml, consSitCTe);
            retorno.SalvarXmlEmDisco(chave, configServico);

            return retorno;
        }

        public async Task<retConsSitCTe> ConsultaProtocoloAsync(string chave, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var consSitCTe = ClassesFactory.CriarconsSitCTe(chave, configServico);

            if (configServico.IsValidaSchemas)
                consSitCTe.ValidarSchema(configServico);

            consSitCTe.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlConsultaProtocolo(configServico);
            var retornoXml = await webService.cteConsultaCTAsync(consSitCTe.CriaRequestWs());

            var retorno = retConsSitCTe.LoadXml(retornoXml.OuterXml, consSitCTe);
            retorno.SalvarXmlEmDisco(chave, configServico);

            return retorno;
        }
    }
}