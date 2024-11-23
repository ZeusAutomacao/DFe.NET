using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeStatusServico;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.StatusServicoMDFe
{
    public class ServicoMDFeStatusServico
    {
        public MDFeRetConsStatServ MDFeStatusServico(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var consStatServMDFe = ClassesFactory.CriaConsStatServMDFe(config);
            consStatServMDFe.ValidarSchema(config);
            consStatServMDFe.SalvarXmlEmDisco(config);

            var webService = WsdlFactory.CriaWsdlMDFeStatusServico(config);
            var retornoXml = webService.mdfeStatusServicoMDF(consStatServMDFe.CriaRequestWs());

            var retorno = MDFeRetConsStatServ.LoadXml(retornoXml.OuterXml, consStatServMDFe);
            retorno.SalvarXmlEmDisco(config);

            return retorno;

        }
    }
}