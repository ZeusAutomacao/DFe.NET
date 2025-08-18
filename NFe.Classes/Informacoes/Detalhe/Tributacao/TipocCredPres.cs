using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum TipocCredPres
    {
        [Description("Aquisição de Produtor Rural não contribuinte.")]
        [XmlEnum("1")]
        ArquisicaoDeProdutorRuralNaoContribuinte = 1,

        [Description("Tomador de serviço de transporte de TAC PF não contrib.")]
        [XmlEnum("2")]
        TomadorDeServicoDeTransporteDeTACPFNaoContribuinte = 2,

        [Description("Aquisição de pessoa física com destino a reciclagem.")]
        [XmlEnum("3")]
        AquisicaoDePessoaFisicaComDestinoAReciclagem = 3,

        [Description("Aquisição de bens móveis de PF não contrib. para revenda (veículos / brechó).")]
        [XmlEnum("4")]
        AquisicaoDeBensMoveisDePFNaoContribuinteParaRevenda = 4,

        [Description("Regime opcional para cooperativa.")]
        [XmlEnum("5")]
        RegimeOpcionalParaCooperativa = 5,
    }
}