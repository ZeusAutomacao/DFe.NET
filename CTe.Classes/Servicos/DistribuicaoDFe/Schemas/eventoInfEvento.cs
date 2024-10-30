using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class eventoInfEvento
    {
        [XmlAttribute()]
        public string Id { get; set; }

        public byte cOrgao { get; set; }

        public byte tpAmb { get; set; }

        public string CNPJ { get; set; }

        public string chCTe { get; set; }

        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxydhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        public uint tpEvento { get; set; }

        public byte nSeqEvento { get; set; }

        public decimal versaoEvento { get; set; }

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public detEvento detEvento { get; set; }
    }
}