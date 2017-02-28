using System.Collections.Generic;
using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Evento.Flags;
using CTeDLL.Servicos.Factory;
using CteEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Eventos
{
    public class EventoCartaCorrecao
    {
        private readonly CteEletronico _cte;
        private readonly int _sequenciaEvento;
        private readonly List<infCorrecao> _infCorrecaos;

        public EventoCartaCorrecao(CteEletronico cte, int sequenciaEvento,
            List<infCorrecao> infCorrecaos)
        {
            _cte = cte;
            _sequenciaEvento = sequenciaEvento;
            _infCorrecaos = infCorrecaos;
        }

        public retEventoCTe AdicionarCorrecoes()
        {
            var eventoCorrecao = ClassesFactory.CriaEvCCeCTe(_infCorrecaos);

            var retorno = new ServicoController().Executar(_cte, _sequenciaEvento, eventoCorrecao, TipoEvento.CartaCorrecao);

            return retorno;
        }
    }
}