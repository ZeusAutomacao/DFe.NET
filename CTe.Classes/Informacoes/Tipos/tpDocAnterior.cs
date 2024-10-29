using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpDocAnterior
    {
        [XmlEnum("00")]
        CTRC = 00,
        [XmlEnum("01")]
        CTAC = 01,
        [XmlEnum("02")]
        ACT = 02,
        [XmlEnum("03")]
        NFModelo7 = 03,
        [XmlEnum("04")]
        NFModelo27 = 04,
        [XmlEnum("05")]
        ConhecimentoAereoNacional = 05,
        [XmlEnum("06")]
        CTMC = 06,
        [XmlEnum("07")]
        ARTE = 07,
        [XmlEnum("08")]
        DTA = 08,
        [XmlEnum("09")]
        ConhecimentoAereoInternacional = 09,
        [XmlEnum("10")]
        ConhecimentoCartaDePorteInternacional = 10,
        [XmlEnum("11")]
        ConhecimentoAvulso = 11,
        [XmlEnum("12")]
        TIF = 12,
        [XmlEnum("13")]
        BL = 13,
        [XmlEnum("99")]
        Outros = 99
    }
}