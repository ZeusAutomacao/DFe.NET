using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoProcessoEmissaoCTe
    {
        [XmlEnum("0")]
        EmissaoAplicativoContribuinte = 0,

        [XmlEnum("1")]
        EmissaoAvulsaPeloFisco = 1,

        [XmlEnum("2")]
        EmissaoAvulsaContribuinteCertificadoSite = 2,

        [XmlEnum("3")]
        EmissaoPeloContribuinteAplicativoFornecidoFisco = 3
    }
}