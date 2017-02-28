using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos.Recepcao.Retorno;
using CTeDLL.Servicos.Factory;

namespace CTeDLL.Servicos.ConsultaRecibo
{
    public class ConsultaReciboServico
    {
        private readonly string _recibo;

        public ConsultaReciboServico(string recibo)
        {
            _recibo = recibo;
        }

        public retConsReciCTe Consultar()
        {
            var consReciCTe = ClassesFactory.CriaConsReciCTe(_recibo);
            consReciCTe.ValidarSchema();
            consReciCTe.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteRetRecepcao();
            var retornoXml = webService.cteRetRecepcao(consReciCTe.CriaRequestWs());

            var retorno = retConsReciCTe.LoadXml(retornoXml.OuterXml, consReciCTe);
            return retorno;
        }
    }
}