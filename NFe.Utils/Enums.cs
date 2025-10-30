using System.ComponentModel;

namespace NFe.Utils
{
    public enum TipoUrlConsultaPublica
    {
        [Description("Endereço para consulta da NFCe através do site")]
        UrlConsulta,

        [Description("Endereço para consulta da NFCe através do QR-Code")]
        UrlQrCode
    }

    public enum VersaoQrCode
    {
        [Description("Versão 1.0 do QR-Code")]
        QrCodeVersao1 = 100,

        [Description("Versão 2.0 do QR-Code")]
        QrCodeVersao2 = 2,

        [Description("Versão 3.0 do QR-Code")]
        QrCodeVersao3 = 3,

    }
}