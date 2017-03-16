using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos;
using CTeDLL.Classes.Servicos.Consulta;
using CTeDLL.Servicos.Factory;

namespace CTe.Servicos.ConsultaProtocolo
{
    public class ConsultaProtcoloServico
    {
        public retConsSitCTe ConsultaProtocolo(string chave)
        {
            var consSitCTe = ClassesFactory.CriarconsSitCTe(chave);
            consSitCTe.ValidarSchema();
            consSitCTe.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlConsultaProtocolo();
            var retornoXml = webService.cteConsultaCT(consSitCTe.CriaRequestWs());

            var retorno = retConsSitCTe.LoadXml(retornoXml.OuterXml, consSitCTe);
            retorno.SalvarXmlEmDisco(chave);

            return retorno;
        }
    }
}