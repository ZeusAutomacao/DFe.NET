using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum TipocCredPres
    {
        [Description("Aquisição de Produtor Rural não contribuinte.")]
        [XmlEnum("01")]
        ArquisicaoDeProdutorRuralNaoContribuinte = 1,

        [Description("Tomador de serviço de transporte de TAC PF não contrib.")]
        [XmlEnum("02")]
        TomadorDeServicoDeTransporteDeTACPFNaoContribuinte = 2,

        [Description("Aquisição de pessoa física com destino a reciclagem.")]
        [XmlEnum("03")]
        AquisicaoDePessoaFisicaComDestinoAReciclagem = 3,

        [Description("Aquisição de bens móveis de PF não contrib. para revenda (veículos / brechó).")]
        [XmlEnum("04")]
        AquisicaoDeBensMoveisDePFNaoContribuinteParaRevenda = 4,

        [Description("Regime opcional para cooperativa.")]
        [XmlEnum("05")]
        RegimeOpcionalParaCooperativa = 5,
    }
}