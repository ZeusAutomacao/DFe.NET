using DFe.Utils;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class infProt
    {

        public byte tpAmb { get; set; }

        public string verAplic { get; set; }

        [XmlElement(DataType = "integer")]
        public string chNFe { get; set; }

        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxyDhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        public ulong nProt { get; set; }

        public string digVal { get; set; }

        public int cStat { get; set; }

        public string xMotivo { get; set; }

        [XmlAttribute()]
        public string Id { get; set; }
    }
}