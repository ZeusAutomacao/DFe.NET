using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe.Contratos
{
    public interface IServicoController
    {
        MDFeRetEventoMDFe Executar(MDFeEletronico mdfe, byte sequenciaEvento, MDFeEventoContainer eventoContainer);
    }
}