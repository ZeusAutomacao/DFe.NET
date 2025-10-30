using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Servicos.Factory;

namespace CTe.Servicos.Eventos
{
    public class EventoDesacordo
    {
        private readonly int _sequenciaEvento;
        private readonly string _cnpj;
        private readonly string _chave;
        private readonly string _indicadorDesacordo;
        private readonly string _observacao;

        public eventoCTe EventoEnviado { get; private set; }
        public retEventoCTe RetornoSefaz { get; private set; }

        public EventoDesacordo(int sequenciaEvento, string chave, string cnpj, string indicadorDesacordo, string observacao)
        {
            _chave = chave;
            _cnpj = cnpj;
            _sequenciaEvento = sequenciaEvento;
            _indicadorDesacordo = indicadorDesacordo;
            _observacao = observacao;
        }

        public retEventoCTe Discordar(ConfiguracaoServico configuracaoServico = null)
        {
            var eventoDiscordar = ClassesFactory.CriaEvPrestDesacordo(_indicadorDesacordo, _observacao);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configuracaoServico);
            RetornoSefaz = new ServicoController().Executar(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configuracaoServico);

            return RetornoSefaz;
        }

        public async Task<retEventoCTe> DiscordarAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var eventoDiscordar = ClassesFactory.CriaEvPrestDesacordo(_indicadorDesacordo, _observacao);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configuracaoServico);
            RetornoSefaz = await new ServicoController().ExecutarAsync(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configuracaoServico);

            return RetornoSefaz;
        }
    }
}