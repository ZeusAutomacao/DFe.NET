using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos.Status;
using CTeDLL.Servicos.Factory;

namespace CTeDLL.Servicos.ConsultaStatus
{
    public class StatusServico
    {
        public retConsStatServCte ConsultaStatus()
        {
            var consStatServCte = ClassesFactory.CriaConsStatServCte();
            consStatServCte.ValidarSchema();
            consStatServCte.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteStatusServico();
            var retornoXml = webService.cteStatusServicoCT(consStatServCte.CriaRequestWs());

            var retorno = retConsStatServCte.LoadXml(retornoXml.OuterXml, consStatServCte);
            return retorno;
        }
    }
}