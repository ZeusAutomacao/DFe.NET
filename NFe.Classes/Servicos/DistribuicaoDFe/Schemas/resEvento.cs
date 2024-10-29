using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class resEvento
    {
        /// <summary>
        /// D02 - Versão do leiaute
        /// </summary>
        [XmlAttribute()]
        public decimal versao { get; set; }

        /// <summary>
        /// D03 - Código do órgão de recepção do Evento. 
        /// Utilizar 91 para identificar o Ambiente Nacional.
        /// </summary>
        public string cOrgao { get; set; }

        /// <summary>
        /// D04 - CNPJ do Emitente
        /// </summary>
        public ulong CNPJ { get; set; }

        /// <summary>
        /// D05 - CPF do Emitente
        /// </summary>
        public ulong CPF { get; set; }

        /// <summary>
        /// D06 - Chave de acesso da NF-e
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string chNFe { get; set; }

        /// <summary>
        /// D07 - Data e hora do evento
        /// </summary>
        [XmlIgnore]
        public DateTime dhEvento { get; set; }

        /// <summary>
        /// Proxy para dhEvento no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhEvento")]
        public string ProxydhEvento
        {
            get { return dhEvento.ParaDataHoraStringUtc(); }
            set { dhEvento = DateTime.Parse(value); }
        }

        /// <summary>
        /// D08 - Código do evento
        /// </summary>
        public string tpEvento { get; set; }

        /// <summary>
        /// D09 - Número sequencial do evento
        /// </summary>
        public string nSeqEvento { get; set; }

        /// <summary>
        /// D10 - Descrição do evento
        /// </summary>
        public string xEvento { get; set; }

        /// <summary>
        /// D11 - Data de autorização do evento
        /// </summary>
        public DateTime dhRecbto { get; set; }

        /// <summary>
        /// D12 - Número de protocolo do evento
        /// </summary>
        public ulong nProt { get; set; }
    }
}