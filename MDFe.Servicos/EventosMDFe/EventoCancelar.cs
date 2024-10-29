using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Factory;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoCancelar
    {
        public MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo, string justificativa)
        {
            var cancelamento = ClassesFactory.CriaEvCancMDFe(protocolo, justificativa);

            var retorno = new ServicoController().Executar(mdfe, sequenciaEvento, cancelamento, MDFeTipoEvento.Cancelamento);

            return retorno;
        }
    }
}