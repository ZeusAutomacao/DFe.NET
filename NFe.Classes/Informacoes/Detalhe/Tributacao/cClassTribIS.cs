using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum cClassTribIS
    {
        // todo não encontrei ainda a descrição oficial para estes códigos

        [Description("Tributada integralmente")]
        [XmlEnum("000001")]
        ctis000001 = 000001
    }
}