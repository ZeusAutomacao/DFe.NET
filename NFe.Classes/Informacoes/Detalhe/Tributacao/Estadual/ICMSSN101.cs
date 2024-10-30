using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMSSN101 : ICMSBasico
    {
        private decimal _pCredSn;
        private decimal _vCredIcmssn;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [XmlElement(Order = 1)]
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12a - Código de Situação da Operação – Simples Nacional
        /// </summary>
        [XmlElement(Order = 2)]
        public Csosnicms CSOSN { get; set; }

        /// <summary>
        ///     N29 - pCredSN - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal pCredSN
        {
            get { return _pCredSn; }
            set { _pCredSn = value.Arredondar(4); }
        }

        /// <summary>
        ///     N30 - Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal vCredICMSSN
        {
            get { return _vCredIcmssn; }
            set { _vCredIcmssn = value.Arredondar(2); }
        }
    }
}