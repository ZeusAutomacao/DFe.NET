using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Evento.Flags;
using CTeDLL.Servicos.Factory;
using CteEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Eventos
{
    public class EventoCancelamento
    {
        private readonly CteEletronico _cte;
        private readonly int _sequenciaEvento;
        private readonly string _numeroProtocolo;
        private readonly string _justificativa;

        public EventoCancelamento(CteEletronico cte, int sequenciaEvento, string numeroProtocolo, string justificativa)
        {
            _cte = cte;
            _sequenciaEvento = sequenciaEvento;
            _numeroProtocolo = numeroProtocolo;
            _justificativa = justificativa;
        }

        public retEventoCTe Cancelar()
        {
            var eventoCancelar = ClassesFactory.CriaEvCancCTe(_justificativa, _numeroProtocolo);

            var retorno = new ServicoController().Executar(_cte, _sequenciaEvento, eventoCancelar, TipoEvento.Cancelamento);

            return retorno;
        }
    }
}