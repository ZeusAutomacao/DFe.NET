using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CteEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Eventos.Contratos
{
    public interface IServicoController
    {
        retEventoCTe Executar(CteEletronico cte, int sequenciaEvento, EventoContainer container, CTeTipoEvento evento, ConfiguracaoServico configuracaoServico = null);

        Task<retEventoCTe> ExecutarAsync(CteEletronico cte, int sequenciaEvento, EventoContainer container,
            CTeTipoEvento cTeTipoEvento, ConfiguracaoServico configuracaoServico = null);
    }
}