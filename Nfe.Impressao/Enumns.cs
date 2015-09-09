using System.ComponentModel;

namespace NFe.Impressao
{
    public enum NfceDetalheVendaNormal
    {
        NaoImprimir = 0,
        UmaLinha = 1,
        DuasLinhas = 2
    }

    public enum NfceDetalheVendaContigencia
    {
        UmaLinha = 1,
        DuasLinhas = 2
    }

    public enum TipoUrlDanfeNfce
    {
        [Description("Endereço para consulta da NFCe através do site")]
        UrlConsulta,

        [Description("Endereço para consulta da NFCe através do QR-Code")]
        UrlQrCode
    }
}
