using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeStatusServico;
using MDFe.Servicos.Factory;

namespace MDFe.Servicos.StatusServicoMDFe
{
    public class ServicoMDFeStatusServico
    {
        public MDFeRetConsStatServ MDFeStatusServico()
        {
            var consStatServMDFe = ClassesFactory.CriaConsStatServMDFe();
            consStatServMDFe.ValidarSchema();
            consStatServMDFe.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlMDFeStatusServico();
            var retornoXml = webService.mdfeStatusServicoMDF(consStatServMDFe.CriaRequestWs());

            var retorno = MDFeRetConsStatServ.LoadXml(retornoXml.OuterXml, consStatServMDFe);
            retorno.SalvarXmlEmDisco();

            return retorno;

        }
    }
}