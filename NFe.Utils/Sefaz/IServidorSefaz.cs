using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz
{
    public interface IServidorSefaz
    {
        Estado EstadoReferente { get; set; }
        VersaoServico EventoCceCancelamento { get; set; }
        VersaoServico NfeRecepcao { get; set; }
        VersaoServico NfeRetornoRecepcao { get; set; }
        VersaoServico NfeConsultaCadastro { get; set; }
        VersaoServico NfeInutilizacao { get; set; }
        VersaoServico NfeConsultaProtocolo { get; set; }
        VersaoServico NfeStatusServico { get; set; }
        VersaoServico NfeAutorizacao { get; set; }
        VersaoServico NfeRetornoAutorizacao { get; set; }
        VersaoServico NfeConsultaDest { get; set; }
        VersaoServico NFeDistribuicaoDFe { get; set; }
        VersaoServico ManifestacaoDestinatario { get; set; }
        VersaoServico VersaoRecepcaoEventoEpec { get; set; }
        VersaoServico VersaoNfceAministracaoCSC { get; set; }
    }
}