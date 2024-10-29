using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpCar
    {
        [XmlEnum("00")]
        NaoAplicavel = 00,
        [XmlEnum("01")]
        Aberta = 01,
        [XmlEnum("02")]
        Fechada = 02,
        [XmlEnum("03")]
        Graneleira = 03,
        [XmlEnum("04")]
        PortaContainer = 04,
        [XmlEnum("05")]
        Sider = 05
    }
}