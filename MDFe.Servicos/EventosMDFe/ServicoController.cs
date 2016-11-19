using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.EventosMDFe.Contratos;
using MDFe.Servicos.Factory;
using MDFe.Utils.Extencoes;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoController : IServicoController
    {
        public MDFeRetEventoMDFe Executar(MDFeEletronico mdfe, byte sequenciaEvento, MDFeEventoContainer eventoContainer)
        {
            var evento = FactoryEvento.CriaEvento(mdfe,
                MDFeTipoEvento.Cancelamento,
                sequenciaEvento,
                eventoContainer);

            evento.ValidarSchema();
            evento.SalvarXmlEmDisco(mdfe.Chave());

            var webService = WsdlFactory.CriaWsdlMDFeRecepcaoEvento();
            var retornoXml = webService.mdfeRecepcaoEvento(evento.CriaXmlRequestWs());

            var retorno = MDFeRetEventoMDFe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(mdfe.Chave());

            return retorno;
        }
    }
}