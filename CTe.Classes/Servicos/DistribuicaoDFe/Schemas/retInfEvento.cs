using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retInfEvento
    {
        public byte tpAmb { get; set; }

        public string verAplic { get; set; }

        public byte cOrgao { get; set; }

        public byte cStat { get; set; }

        public string xMotivo { get; set; }

        public string chCTe { get; set; }

        public uint tpEvento { get; set; }

        public string xEvento { get; set; }

        public byte nSeqEvento { get; set; }

        public string CNPJDest { get; set; }

        public string emailDest { get; set; }
        
        [XmlIgnore]
        public DateTimeOffset dhRegEvento { get; set; }

        [XmlElement(ElementName = "dhRegEvento")]
        public string ProxydhRegEvento
        {
            get { return dhRegEvento.ParaDataHoraStringUtc(); }
            set { dhRegEvento = DateTimeOffset.Parse(value); }
        }

        public string nProt { get; set; }
    }
}