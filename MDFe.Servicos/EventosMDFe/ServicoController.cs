using MDFe.Classes.Extencoes;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.EventosMDFe.Contratos;
using MDFe.Servicos.Factory;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoController : IServicoController
    {
        public MDFeRetEventoMDFe Executar(MDFeEletronico mdfe, byte sequenciaEvento, MDFeEventoContainer eventoContainer, MDFeTipoEvento tipoEvento)
        {
            var evento = FactoryEvento.CriaEvento(mdfe,
                tipoEvento,
                sequenciaEvento,
                eventoContainer);


            string chave = mdfe.Chave();

            evento.ValidarSchema();
            evento.SalvarXmlEmDisco(chave);

            var webService = WsdlFactory.CriaWsdlMDFeRecepcaoEvento();
            var retornoXml = webService.mdfeRecepcaoEvento(evento.CriaXmlRequestWs());

            var retorno = MDFeRetEventoMDFe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(chave);

            return retorno;
        }
    }
}