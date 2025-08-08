using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Identificacao
{
    public enum tpOperGov
    {
        [Description("Fornecimento")]
        [XmlEnum("1")]
        Fornecimento = 1,

        [Description("Recebimento do pagamento, conforme fato gerador do IBS/CBS definido no Art. 10 § 2º")]
        [XmlEnum("2")]
        RecebimentoPagamento = 2,
    }
}