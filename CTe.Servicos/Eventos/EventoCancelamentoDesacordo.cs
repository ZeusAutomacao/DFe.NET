using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Servicos.Factory;
using System.Threading.Tasks;

namespace CTe.Servicos.Eventos
{
    public class EventoCancelamentoDesacordo
    {
        private readonly int _sequenciaEvento;
        private readonly string _cnpj;
        private readonly string _chave;
        private readonly string _nProtEvPrestDes;

        public eventoCTe EventoEnviado { get; private set; }
        public retEventoCTe RetornoSefaz { get; private set; }

        public EventoCancelamentoDesacordo(int sequenciaEvento, string chave, string cnpj, string nProtEvPrestDes)
        {
            _chave = chave;
            _cnpj = cnpj;
            _sequenciaEvento = sequenciaEvento;
            _nProtEvPrestDes = nProtEvPrestDes;
        }

        public retEventoCTe CancelarDesacordo(ConfiguracaoServico configuracaoServico = null, DFe.Classes.Entidades.Estado? orgaoEmissor = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var eventoCancelaDiscordar = ClassesFactory.CriaEvCancPrestDesacordo(_nProtEvPrestDes);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.CancelamentoPrestacaodoServicoemDesacordo, _sequenciaEvento, _chave, _cnpj, eventoCancelaDiscordar, configServico, orgaoEmissor);
            RetornoSefaz = new ServicoController().Executar(CTeTipoEvento.CancelamentoPrestacaodoServicoemDesacordo, _sequenciaEvento, _chave, _cnpj, eventoCancelaDiscordar, configServico, orgaoEmissor);

            return RetornoSefaz;
        }

        public async Task<retEventoCTe> CancelarDesacordoAsync(ConfiguracaoServico configuracaoServico = null, DFe.Classes.Entidades.Estado? orgaoEmissor = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var eventoCancelaDiscordar = ClassesFactory.CriaEvCancPrestDesacordo(_nProtEvPrestDes);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.CancelamentoPrestacaodoServicoemDesacordo, _sequenciaEvento, _chave, _cnpj, eventoCancelaDiscordar, configServico, orgaoEmissor);
            RetornoSefaz = await new ServicoController().ExecutarAsync(CTeTipoEvento.CancelamentoPrestacaodoServicoemDesacordo, _sequenciaEvento, _chave, _cnpj, eventoCancelaDiscordar, configServico, orgaoEmissor);

            return RetornoSefaz;
        }
    }
}
