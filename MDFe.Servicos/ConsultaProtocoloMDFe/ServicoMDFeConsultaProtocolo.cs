using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Servicos.Factory;

namespace MDFe.Servicos.ConsultaProtocoloMDFe
{
    public class ServicoMDFeConsultaProtocolo
    {
        public MDFeRetConsSitMDFe MDFeConsultaProtocolo(string chave)
        {
            var consSitMdfe = ClassesFactory.CriarConsSitMDFe(chave);
            consSitMdfe.ValidarSchema();
            consSitMdfe.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlMDFeConsulta();
            var retornoXml = webService.mdfeConsultaMDF(consSitMdfe.CriaRequestWs());

            var retorno = MDFeRetConsSitMDFe.LoadXml(retornoXml.OuterXml, consSitMdfe);
            retorno.SalvarXmlEmDisco(chave);

            return retorno;
        }
    }
}