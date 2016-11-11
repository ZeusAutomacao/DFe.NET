using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{
    public enum MDFeIdentificacaoProcessoEmissao
    {
        [XmlEnum("0")]
        EmissaoComAplicativoContribuinte = 0,
        [XmlEnum("1")]
        EmissoPeloContribuinteComAplicativoFornecidoPeloFisco = 1
    }
}