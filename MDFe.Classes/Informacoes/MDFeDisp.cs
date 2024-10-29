using System;
using System.Xml.Serialization;
using DFe.Classes;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeDisp
    {
        private decimal _vValePed;

        /// <summary>
        /// 3 - CNPJ da empresa fornecedora do ValePedágio
        /// </summary>
        [XmlElement(ElementName = "CNPJForn")]
        public string CNPJForn { get; set; }

        /// <summary>
        /// 3 - CNPJ do responsável pelo pagamento do Vale-Pedágio
        /// </summary>
        [XmlElement(ElementName = "CNPJPg")]
        public string CNPJPg { get; set; }

        public string CPFPg { get; set; }

        /// <summary>
        /// 3 - Número do comprovante de compra 
        /// </summary>
        [XmlElement(ElementName = "nCompra")]
        public string NCompra { get; set; }

        public decimal vValePed
        {
            get { return _vValePed.Arredondar(2); }
            set { _vValePed = value.Arredondar(2); }
        }

        public tpValePed? tpValePed { get; set; }

        public bool tpValePedSpecified { get { return tpValePed.HasValue; } }
    }
}