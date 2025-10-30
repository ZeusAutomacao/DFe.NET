using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CteEletronico = CTe.Classes.CTe;
using CteEletronicoOS = CTe.CTeOSClasses.CTeOS;

namespace CTe.Servicos.Eventos
{
    public class EventoCancelamento
    {
        private readonly CteEletronico _cte;
        private readonly CteEletronicoOS _cteOs;

        private readonly int _sequenciaEvento;
        private readonly string _numeroProtocolo;
        private readonly string _justificativa;

        public eventoCTe EventoEnviado { get; private set; }
        public retEventoCTe RetornoSefaz { get; private set; }

        public EventoCancelamento(CteEletronico cte, int sequenciaEvento, string numeroProtocolo, string justificativa)
        {
            _cte = cte;
            _sequenciaEvento = sequenciaEvento;
            _numeroProtocolo = numeroProtocolo;
            _justificativa = justificativa;
        }

        public EventoCancelamento(CteEletronicoOS cte, int sequenciaEvento, string numeroProtocolo, string justificativa)
        {
            _cteOs = cte;
            _sequenciaEvento = sequenciaEvento;
            _numeroProtocolo = numeroProtocolo;
            _justificativa = justificativa;
        }

        public retEventoCTe Cancelar(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var evento = ClassesFactory.CriaEvCancCTe(_justificativa, _numeroProtocolo);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Cancelamento, _sequenciaEvento, _cte.Chave(), _cte.infCte.emit.CNPJ, evento, configServico);
            RetornoSefaz = new ServicoController().Executar(_cte, _sequenciaEvento, evento, CTeTipoEvento.Cancelamento, configServico);

            return RetornoSefaz;
        }

        public retEventoCTe CancelarOs(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var evento = ClassesFactory.CriaEvCancCTe(_justificativa, _numeroProtocolo);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Cancelamento, _sequenciaEvento, _cteOs.Chave(), _cteOs.InfCte.emit.CNPJ, evento, configServico);
            RetornoSefaz = new ServicoController().Executar(_cteOs, _sequenciaEvento, evento, CTeTipoEvento.Cancelamento, configServico);

            return RetornoSefaz;
        }

        public async Task<retEventoCTe> CancelarAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var evento = ClassesFactory.CriaEvCancCTe(_justificativa, _numeroProtocolo);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Cancelamento, _sequenciaEvento, _cte.Chave(), _cte.infCte.emit.CNPJ, evento, configServico);
            RetornoSefaz = await new ServicoController().ExecutarAsync(_cte, _sequenciaEvento, evento, CTeTipoEvento.Cancelamento, configServico);

            return RetornoSefaz;
        }
    }
}