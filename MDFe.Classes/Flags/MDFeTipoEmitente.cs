using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{   
    public enum MDFeTipoEmitente
    {
        [XmlEnum("1")]
        PrestadorServicoDeTransporte = 1,
        [XmlEnum("2")]
        TransportadorCargaPropria = 2    
    }
}