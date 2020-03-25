using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public enum tpComp
    {
        [XmlEnum("01")]
        ValePedagio = 01,

        [XmlEnum("02")]
        ImpostosTaxasEContribuicoes = 02,

        [XmlEnum("03")]
        DespesasBancariasEmiosDePagamentoOutras = 03,

        [XmlEnum("99")]
        Outros = 99
    }
}