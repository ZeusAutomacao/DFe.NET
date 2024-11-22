using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeRetRecepcao;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.RetRecepcaoMDFe
{
    public class ServicoMDFeRetRecepcao
    {
        public MDFeRetConsReciMDFe MDFeRetRecepcao(string numeroRecibo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var consReciMdfe = ClassesFactory.CriaConsReciMDFe(numeroRecibo, config);
            consReciMdfe.ValidaSchema(config);
            consReciMdfe.SalvarXmlEmDisco(config);

            var webService = WsdlFactory.CriaWsdlMDFeRetRecepcao(config);
            var retornoXml = webService.mdfeRetRecepcao(consReciMdfe.CriaRequestWs());

            var retorno = MDFeRetConsReciMDFe.LoadXml(retornoXml.OuterXml, consReciMdfe);
            retorno.SalvarXmlEmDisco(config);

            return retorno;
        }
    }
}