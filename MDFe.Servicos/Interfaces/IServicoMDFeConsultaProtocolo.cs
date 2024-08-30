using MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.Interfaces
{
    public interface IServicoMDFeConsultaProtocolo
    {
        MDFeRetConsSitMDFe MDFeConsultaProtocolo(string chave, MDFeConfiguracao cfgMdfe);
    }
}
