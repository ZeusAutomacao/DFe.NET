using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Evento.Flags;
using CteEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Eventos.Contratos
{
    public interface IServicoController
    {
        retEventoCTe Executar(CteEletronico cte, int sequenciaEvento, IEventoContainer container, TipoEvento evento);
    }
}