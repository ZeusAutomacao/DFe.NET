using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeIdentificacaoProcessoEmissao
    {
        [XmlEnum("0")]
        EmissaoComAplicativoContribuinte = 0,
        [XmlEnum("3")]
        EmissoPeloContribuinteComAplicativoFornecidoPeloFisco = 3
    }
}