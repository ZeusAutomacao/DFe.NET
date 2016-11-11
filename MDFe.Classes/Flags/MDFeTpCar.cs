using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{
    public enum MDFeTpCar
    {
        [XmlEnum("00")]
        NaoAplicavel = 00,
        [XmlEnum("01")]
        Aberta = 01,
        [XmlEnum("02")]
        FechadaBau = 02,
        [XmlEnum("03")]
        Granelera = 03,
        [XmlEnum("04")]
        PortaContainer = 04,
        [XmlEnum("05")]
        Sider = 05
    }
}