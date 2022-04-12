using DFe.Utils;
using System;
using System.Xml.Serialization;

namespace Shared.NFe.Classes.Servicos.Evento
{
    public sealed class itensAverbados
    {
        /// <summary>
        /// Data do Embarque no formato AAAA-MM-DDThh:mm:ssTZD
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset DhEmbarque { get; set; }

        [XmlElement(ElementName = "dhEmbarque")]
        public string ProxyDhEmbarque
        {
            get { return DhEmbarque.ParaDataHoraStringUtc(); }
            set { DhEmbarque = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Proxy Data da averbação no formato AAAA-MM-DDThh:mm:ssTZD
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset DhAverbacao { get; set; }

        [XmlElement(ElementName = "dhAverbacao")]
        public string ProxyDhAverbacao
        {
            get { return DhAverbacao.ParaDataHoraStringUtc(); }
            set { DhAverbacao = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Número Identificador da Declaração Única do Comércio Exterior associada - [0-9]{2}BR[0-9]{10}
        /// </summary>
        [XmlElement(ElementName = "nDue")]
        public string NDue { get; set; }

        /// <summary>
        /// Número do item da NF-e averbada - [0-9]{1,3}
        /// </summary>
        [XmlElement(ElementName = "nItem")]
        public string NItem { get; set; }

        /// <summary>
        /// Informação do número do item na Declaração de Exportação associada a averbação. - [0-9]{1,4}
        /// </summary>
        [XmlElement(ElementName = "nItemDue")]
        public string NItemDue { get; set; }

        /// <summary>
        /// Quantidade averbada do item na unidade tributária (campo uTrib) - TDec_1104Neg
        /// </summary>
        [XmlElement(ElementName = "qItem")]
        public decimal QItem { get; set; }

        /// <summary>
        ///  Motivo da Alteração
        ///    1 - Exportação Averbada;
        ///    2 - Retificação da Quantidade Averbada;
        ///    3 - Cancelamento da Exportação;
        /// </summary>
        [XmlElement(ElementName = "motAlteracao")]
        public int MotAlteracao { get; set; }
    }
}
