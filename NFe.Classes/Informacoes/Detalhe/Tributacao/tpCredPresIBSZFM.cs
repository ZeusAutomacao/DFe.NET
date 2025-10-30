using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum tpCredPresIBSZFM
    {
        [Description("Sem Crédito Presumido")]
        [XmlEnum("0")]
        tpCredPresIbsZfm0,

        [Description("Bens de consumo final (55%)")]
        [XmlEnum("1")]
        tpCredPresIbsZfm1,

        [Description("Bens de capital (75%)")]
        [XmlEnum("2")]
        tpCredPresIbsZfm2,

        [Description("Bens intermediários (90,25%)")]
        [XmlEnum("3")]
        tpCredPresIbsZfm3,

        [Description("Bens de informática e outros definidos em legislação (100%)")]
        [XmlEnum("4")]
        tpCredPresIbsZfm4
    }
}