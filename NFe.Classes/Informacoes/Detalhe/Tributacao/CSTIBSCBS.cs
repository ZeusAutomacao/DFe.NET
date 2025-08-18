using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum CSTIBSCBS
    {
        [Description("Tributação integral")]
        [XmlEnum("000")]
        cst000 = 000,

        [Description("Tributação com alíquotas uniformes")]
        [XmlEnum("010")]
        cst010 = 010,

        [Description("Tributação com alíquotas uniformes reduzidas")]
        [XmlEnum("011")]
        cst011 = 011,

        [Description("Alíquota reduzida")]
        [XmlEnum("200")]
        cst200 = 200,

        [Description("Redução de alíquota com redutor de base de cálculo")]
        [XmlEnum("210")]
        cst210 = 210,

        [Description("Alíquota fixa")]
        [XmlEnum("220")]
        cst220 = 220,

        [Description("Alíquota fixa rateada")]
        [XmlEnum("221")]
        cst221 = 221,

        [Description("Redução de Base de Cálculo")]
        [XmlEnum("222")]
        cst222 = 222,

        [Description("Isenção")]
        [XmlEnum("400")]
        cst400 = 400,

        [Description("Imunidade e não incidência")]
        [XmlEnum("410")]
        cst410 = 410,

        [Description("Diferimento")]
        [XmlEnum("510")]
        cst510 = 510,

        [Description("Suspensão")]
        [XmlEnum("550")]
        cst550 = 550,

        [Description("Tributação Monofásica")]
        [XmlEnum("620")]
        cst620 = 620,

        [Description("Transferência de crédito")]
        [XmlEnum("800")]
        cst800 = 800,

        [Description("Ajustes")]
        [XmlEnum("810")]
        cst810 = 810,

        [Description("Tributação em declaração de regime específico")]
        [XmlEnum("820")]
        cst820 = 820,

        [Description("Exclusão da Base de Cálculo")]
        [XmlEnum("830")]
        cst830 = 830,
    }
}