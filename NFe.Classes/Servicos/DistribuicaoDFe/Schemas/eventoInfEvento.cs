using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class eventoInfEvento
    {
        public byte cOrgao { get; set; }

        public byte tpAmb { get; set; }

        public string CNPJ { get; set; }

        public string chNFe { get; set; }

        public DateTime dhEvento { get; set; }

        public uint tpEvento { get; set; }

        public byte nSeqEvento { get; set; }

        public decimal verEvento { get; set; }

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public detEvento detEvento { get; set; }

        [XmlAttribute()]
        public string Id { get; set; }
    }
}