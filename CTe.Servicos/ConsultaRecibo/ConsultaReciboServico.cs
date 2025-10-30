using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Servicos.Factory;
using CTe.Utils.Extencoes;

namespace CTe.Servicos.ConsultaRecibo
{
    public class ConsultaReciboServico
    {
        private readonly string _recibo;

        public ConsultaReciboServico(string recibo)
        {
            _recibo = recibo;
        }

        public retConsReciCTe Consultar(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var consReciCTe = ClassesFactory.CriaConsReciCTe(_recibo, configServico);

            if (configServico.IsValidaSchemas)
                consReciCTe.ValidarSchema(configServico);

            consReciCTe.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlCteRetRecepcao(configServico);
            var retornoXml = webService.cteRetRecepcao(consReciCTe.CriaRequestWs());

            var retorno = retConsReciCTe.LoadXml(retornoXml.OuterXml, consReciCTe);
            retorno.SalvarXmlEmDisco(configServico);

            return retorno;
        }

        public async Task<retConsReciCTe> ConsultarAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var consReciCTe = ClassesFactory.CriaConsReciCTe(_recibo, configServico);

            if (configServico.IsValidaSchemas)
                consReciCTe.ValidarSchema(configServico);

            consReciCTe.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlCteRetRecepcao(configServico);
            var retornoXml = await webService.cteRetRecepcaoAsync(consReciCTe.CriaRequestWs());

            var retorno = retConsReciCTe.LoadXml(retornoXml.OuterXml, consReciCTe);
            retorno.SalvarXmlEmDisco(configServico);

            return retorno;
        }
    }
}