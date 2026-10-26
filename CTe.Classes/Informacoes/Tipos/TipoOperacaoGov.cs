using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum TipoOperacaoGov
    {
        [Description("Fornecimento com pagamento posterior")]
        [XmlEnum("1")]
        FornecimentoComPagamentoPosterior = 1,

        [Description("Recebimento do pagamento com fornecimento já realizado")]
        [XmlEnum("2")]
        RecebimentoDoPagamentoComFornecimentoJaRealizado = 2,

        [Description("Fornecimento com pagamento já realizado")]
        [XmlEnum("3")]
        FornecimentoComPagamentoJaRealizado = 3,

        [Description("Recebimento do pagamento com fornecimento posterior")]
        [XmlEnum("4")]
        RecebimentoDoPagamentoComFornecimentoPosterior = 4
    }
}
