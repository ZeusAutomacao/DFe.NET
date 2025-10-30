using System.Collections.Generic;
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
    public class EventoCartaCorrecao
    {
        private readonly CteEletronico _cte;
        private readonly CteEletronicoOS _cteOs;
        private readonly int _sequenciaEvento;
        private readonly List<infCorrecao> _infCorrecaos;

        public eventoCTe EventoEnviado { get; private set; }
        public retEventoCTe RetornoSefaz { get; private set; }

        public EventoCartaCorrecao(CteEletronico cte, int sequenciaEvento,
            List<infCorrecao> infCorrecaos)
        {
            _cte = cte;
            _sequenciaEvento = sequenciaEvento;
            _infCorrecaos = infCorrecaos;
        }

        public EventoCartaCorrecao(CteEletronicoOS cte, int sequenciaEvento, List<infCorrecao> infCorrecaos)
        {
            _cteOs = cte;
            _sequenciaEvento = sequenciaEvento;
            _infCorrecaos = infCorrecaos;
        }

        public retEventoCTe AdicionarCorrecoes(ConfiguracaoServico configuracaoServico = null)
        {
            var eventoCorrecao = ClassesFactory.CriaEvCCeCTe(_infCorrecaos);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.CartaCorrecao, _sequenciaEvento, _cte.Chave(), _cte.infCte.emit.CNPJ, eventoCorrecao, configuracaoServico);
            RetornoSefaz = new ServicoController().Executar(_cte, _sequenciaEvento, eventoCorrecao, CTeTipoEvento.CartaCorrecao, configuracaoServico);

            return RetornoSefaz;
        }

        public retEventoCTe AdicionarCorrecoesOs(ConfiguracaoServico configuracaoServico = null)
        {
            var eventoCorrecao = ClassesFactory.CriaEvCCeCTe(_infCorrecaos);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.CartaCorrecao, _sequenciaEvento, _cteOs.Chave(), _cteOs.InfCte.emit.CNPJ, eventoCorrecao, configuracaoServico);
            RetornoSefaz = new ServicoController().Executar(_cteOs, _sequenciaEvento, eventoCorrecao, CTeTipoEvento.CartaCorrecao, configuracaoServico);

            return RetornoSefaz;
        }

        public async Task<retEventoCTe> AdicionarCorrecoesAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var eventoCorrecao = ClassesFactory.CriaEvCCeCTe(_infCorrecaos);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.CartaCorrecao, _sequenciaEvento, _cte.Chave(), _cte.infCte.emit.CNPJ, eventoCorrecao, configuracaoServico);
            RetornoSefaz = await new ServicoController().ExecutarAsync(_cte, _sequenciaEvento, eventoCorrecao, CTeTipoEvento.CartaCorrecao, configuracaoServico);

            return RetornoSefaz;
        }
    }
}