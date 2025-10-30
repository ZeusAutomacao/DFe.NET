using DFe.Classes.Entidades;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoEncerramento
    {
        public MDFeRetEventoMDFe MDFeEventoEncerramento(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var encerramento = ClassesFactory.CriaEvEncMDFe(mdfe, protocolo);

            var retorno = new ServicoController().Executar(mdfe, sequenciaEvento, encerramento, MDFeTipoEvento.Encerramento, config);

            return retorno;
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramento(MDFeEletronico mdfe, Estado estadoEncerramento, long codigoMunicipioEncerramento, byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var encerramento = ClassesFactory.CriaEvEncMDFe(estadoEncerramento, codigoMunicipioEncerramento, protocolo);

            var retorno = new ServicoController().Executar(mdfe, sequenciaEvento, encerramento, MDFeTipoEvento.Encerramento, config);

            return retorno;
        }
    }
}