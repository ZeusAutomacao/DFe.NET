using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS
{
    public enum tpFretamento
    {
        [XmlEnum("1")]
        Eventual = 1,

        [XmlEnum("2")]
        Continuo = 2
    }
}