using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe.Contratos
{
    public interface IServicoController
    {
        MDFeRetEventoMDFe Executar(MDFeEletronico mdfe, byte sequenciaEvento, MDFeEventoContainer eventoContainer, MDFeTipoEvento tipoEvento);
    }
}