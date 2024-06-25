using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfBanc
    {
        /// <summary>
        /// 1 - Número do Banco.
        /// </summary>
        [XmlElement(ElementName = "codBanco")]
        public string CodBanco { get; set; }

        /// <summary>
        /// 1 - Número da Agência.
        /// </summary>
        [XmlElement(ElementName = "codAgencia")]
        public string CodAgencia { get; set; }

        /// <summary>
        /// 1 -´Número do CNPJ da Instituição de
        /// pagamento Eletrônico do frete.
        /// </summary>
        [XmlElement(ElementName = "CNPJIPEF")]
        public string CNPJIPEF { get; set; }

        /// <summary>
        /// 1 -Informar a chave PIX para recebimento do frete.
        /// Pode ser email, CPF/ CNPJ (somente numeros), Telefone com a seguinte formatação
        /// (+5599999999999) ou a chave aleatória gerada pela instituição.
        /// </summary>
        [XmlElement(ElementName = "PIX")]
        public string PIX { get; set; }
    }
}