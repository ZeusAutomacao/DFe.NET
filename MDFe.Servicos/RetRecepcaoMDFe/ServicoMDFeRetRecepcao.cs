using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeRetRecepcao;
using MDFe.Servicos.Factory;

namespace MDFe.Servicos.RetRecepcaoMDFe
{
    public class ServicoMDFeRetRecepcao
    {
        public MDFeRetConsReciMDFe MDFeRetRecepcao(string numeroRecibo)
        {
            var consReciMdfe = ClassesFactory.CriaConsReciMDFe(numeroRecibo);
            consReciMdfe.ValidaSchema();
            consReciMdfe.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlMDFeRetRecepcao();
            var retornoXml = webService.mdfeRetRecepcao(consReciMdfe.CriaRequestWs());

            var retorno = MDFeRetConsReciMDFe.LoadXml(retornoXml.OuterXml, consReciMdfe);
            retorno.SalvarXmlEmDisco();

            return retorno;
        }
    }
}