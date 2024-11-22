using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.ConsultaProtocoloMDFe
{
    public class ServicoMDFeConsultaProtocolo
    {
        public MDFeRetConsSitMDFe MDFeConsultaProtocolo(string chave, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var consSitMdfe = ClassesFactory.CriarConsSitMDFe(chave, config);
            consSitMdfe.ValidarSchema(config);
            consSitMdfe.SalvarXmlEmDisco(config);

            var webService = WsdlFactory.CriaWsdlMDFeConsulta(config);
            var retornoXml = webService.mdfeConsultaMDF(consSitMdfe.CriaRequestWs());

            var retorno = MDFeRetConsSitMDFe.LoadXml(retornoXml.OuterXml, consSitMdfe);
            retorno.SalvarXmlEmDisco(chave, config);

            return retorno;
        }
    }
}