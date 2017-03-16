using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Evento.Flags;
using CTeDLL.Servicos.Eventos.Contratos;
using CTeDLL.Servicos.Factory;
using CTeDLL.Utils.Evento;
using CteEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Eventos
{
    public class ServicoController : IServicoController
    {
        public retEventoCTe Executar(CteEletronico cte, int sequenciaEvento, EventoContainer container, TipoEvento tipoEvento)
        {
            var evento = FactoryEvento.CriaEvento(cte, tipoEvento, sequenciaEvento, container);
            evento.Assina();
            evento.ValidarSchema();
            evento.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteEvento();
            var retornoXml = webService.cteRecepcaoEvento(evento.CriaXmlRequestWs());

            var retorno = retEventoCTe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco();

            return retorno;
        }
    }
}