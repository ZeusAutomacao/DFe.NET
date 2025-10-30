using MDFe.Classes.Extencoes;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.EventosMDFe.Contratos;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoController : IServicoController
    {
        public MDFeRetEventoMDFe Executar(MDFeEletronico mdfe, byte sequenciaEvento, MDFeEventoContainer eventoContainer, MDFeTipoEvento tipoEvento, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var evento = FactoryEvento.CriaEvento(mdfe,
                tipoEvento,
                sequenciaEvento,
                eventoContainer,
                config);


            string chave = mdfe.Chave();

            evento.ValidarSchema(config);
            evento.SalvarXmlEmDisco(chave, config);

            var webService = WsdlFactory.CriaWsdlMDFeRecepcaoEvento(config);
            var retornoXml = webService.mdfeRecepcaoEvento(evento.CriaXmlRequestWs());

            var retorno = MDFeRetEventoMDFe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(chave, config);

            return retorno;
        }
    }
}