using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum indIEToma
    {
        [XmlEnum("1")]
        ContribuinteIcms,
        [XmlEnum("2")]
        ContribuinteIsentoDeInscricao,
        [XmlEnum("9")]
        NaoContribuinte
    }
}