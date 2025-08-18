using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum CSTIS
    {
        [Description("Tributada integralmente")]
        [XmlEnum("000")]
        Is000
    }
}