using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{   
    public enum MDFeTipoEmitente
    {
        [XmlEnum("1")]
        PrestadorServicoDeTransporte = 1,
        [XmlEnum("2")]
        TransportadorCargaPropria = 2    
    }
}