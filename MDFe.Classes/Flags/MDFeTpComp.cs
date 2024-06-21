using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeTpComp
    {
        [XmlEnum("01")]
        ValePedagio = 01,

        [XmlEnum("02")]
        ImpostosTaxasEContribuicoes = 02,

        [XmlEnum("03")]
        DespesasBancariasEMeiosDePagamentoOutras = 03,

        [XmlEnum("99")]
        Outros = 99
    }
}